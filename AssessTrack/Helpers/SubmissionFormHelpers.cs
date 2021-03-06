﻿using System;
using System.Web;
using System.Web.Mvc;
using AssessTrack.Models;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using System.Collections.Specialized;

namespace AssessTrack.Helpers
{
    public static class SubmissionFormHelpers
    {
        public static string RenderAssessmentGradingForm(this HtmlHelper helper, SubmissionRecord submission)
        {
            NameValueCollection answers = new NameValueCollection();
            foreach (Response response in submission.Responses)
            {
                answers.Add(response.AnswerID.ToString(), response.ResponseText);
            }

            NameValueCollection scores = new NameValueCollection();
            if (helper.ViewContext.HttpContext.Request.Form.Count > 0)
            {
                foreach (string key in helper.ViewContext.HttpContext.Request.Form.AllKeys)
                {
                    if (key.StartsWith("score-"))
                    {
                        scores.Add(key, helper.ViewContext.HttpContext.Request.Form[key]);
                    }
                }
            }
            else
            {
                foreach (Response response in submission.Responses)
                {
                    scores.Add("score-" + response.AnswerID.ToString(), (response.Score ?? 0.0).ToString());
                }
            }

            NameValueCollection comments = new NameValueCollection();
            if (helper.ViewContext.HttpContext.Request.Form.Count > 0)
            {
                foreach (string key in helper.ViewContext.HttpContext.Request.Form.AllKeys)
                {
                    if (key.StartsWith("comment-"))
                    {
                        comments.Add(key, helper.ViewContext.HttpContext.Request.Form[key]);
                    }
                }
            }
            else
            {
                foreach (Response response in submission.Responses)
                {
                    comments.Add("comment-" + response.AnswerID.ToString(), response.Comment ?? "");
                }
            }

            return helper.RenderAssessmentForm(submission.Assessment, "~/Content/grade.xsl", answers, scores, comments);

        }

        public static string RenderAssessmentViewForm(this HtmlHelper helper, SubmissionRecord submission)
        {
            NameValueCollection answers = new NameValueCollection();
            NameValueCollection scores = new NameValueCollection();
            NameValueCollection comments = new NameValueCollection();
            foreach (Response response in submission.Responses)
            {
                answers.Add(response.AnswerID.ToString(), response.ResponseText);
                scores.Add("score-" + response.AnswerID.ToString(), (response.Score ?? 0.0).ToString());
                comments.Add("comment-" + response.AnswerID.ToString(), response.Comment ?? "");
            }

            return helper.RenderAssessmentForm(submission.Assessment, "~/Content/view.xsl", answers, scores, comments);

        }

        public static string RenderImportForm(Assessment assessment)
        {
            return RenderAssessmentForm(assessment, "~/Content/import.xsl", null, null, null);
        }

        public static string RenderAssessmentForm(this HtmlHelper helper, Assessment assessment, string stylesheet, NameValueCollection answers, NameValueCollection inputs)
        {
            return RenderAssessmentForm(helper, assessment, stylesheet, answers, inputs, null);
        }

        public static string RenderAssessmentForm(this HtmlHelper helper, Assessment assessment, string stylesheet, NameValueCollection answers, NameValueCollection inputs, NameValueCollection comments)
        {
            return RenderAssessmentForm(assessment, stylesheet, answers, inputs, comments);
        }

        public static string RenderAssessmentForm(Assessment assessment, string stylesheet, NameValueCollection answers, NameValueCollection inputs, NameValueCollection comments)
        {
            
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(HttpContext.Current.Server.MapPath(stylesheet));

            //Prepare the xml output writer
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            StringWriter sw = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sw, settings);

            //Load the xml document
            XmlDocument assessmentData = new XmlDocument();
            assessmentData.LoadXml(assessment.Data);
            XmlNodeReader reader = new XmlNodeReader(assessmentData);

            //Perform the transform
            xslt.Transform(reader, writer);
            string markup = sw.ToString();

            XmlDocument transformedData = new XmlDocument();
            transformedData.LoadXml(markup);

            //Insert answers into the input fields if available
            if (answers != null && answers.Count > 0)
            {
                foreach (Answer answer in assessment.Answers)
                {
                    XmlNode answerNode;
                    //Answernode may be null in some instances, mainly if an answer is deleted from
                    //an assessment it will still exist in the db but not in the xml
                    if (answer.Type == "short-answer")
                    {
                        answerNode = transformedData.SelectSingleNode(String.Format("//input[@id='{0}']", answer.AnswerID.ToString()));
                        if (answerNode != null)
                        {
                            XmlAttribute value = transformedData.CreateAttribute("value");
                            value.Value = answers[answer.AnswerID.ToString()];
                            answerNode.Attributes.Append(value);
                        }
                    }
                    else if (answer.Type == "attachment")
                    {
                        if (string.IsNullOrEmpty(answers[answer.AnswerID.ToString()]))
                            continue;
                        answerNode = transformedData.SelectSingleNode(String.Format("//p[@id='download-{0}']", answer.AnswerID.ToString()));
                        //For submit and import this will be null, for view and grade there will be a paragraph to insert a download link
                        if (answerNode != null)
                        {
                            answerNode.InnerXml = answers[answer.AnswerID.ToString()];
                        }
                    }
                    else if (answer.Type == "long-answer")
                    {
                        answerNode = transformedData.SelectSingleNode(String.Format("//textarea[@id='{0}']", answer.AnswerID.ToString()));
                        if (answerNode != null)
                        {
                            answerNode.InnerText = answers[answer.AnswerID.ToString()];
                        }
                    }
                    else if (answer.Type == "code-answer")
                    {
                        answerNode = transformedData.SelectSingleNode(String.Format("//pre[@id='{0}']", answer.AnswerID.ToString()));
                        if (answerNode != null)
                        {
                            answerNode.InnerText = answers[answer.AnswerID.ToString()];
                        }
                    }
                    else if (answer.Type == "multichoice")
                    {
                        string escapedAnswer = System.Security.SecurityElement.Escape(answers[answer.AnswerID.ToString()]);
                        answerNode = transformedData.SelectSingleNode(String.Format(@"//input[starts-with(@id,'{0}')][@value=""{1}""]", answer.AnswerID.ToString(), escapedAnswer));
                        if (answerNode != null)
                        {
                            XmlAttribute checkedAttr = transformedData.CreateAttribute("checked");
                            checkedAttr.Value = "true";
                            answerNode.Attributes.Append(checkedAttr);
                        }
                    }
                }
            }
            
            //insert any other form input data
            if (inputs != null && inputs.Count > 0)
            {
                XmlNode inputNode;
                foreach (string id in inputs.AllKeys)
                {
                    //This assumes all inputs are "input" elements with type="text"
                    string nodePath = string.Format("//node()[@name='{0}']", id);
                    inputNode = transformedData.SelectSingleNode(nodePath);
                    XmlAttribute value = transformedData.CreateAttribute("value");
                    value.Value = inputs[id];
                    inputNode.Attributes.Append(value);
                }
            }
            if (comments != null && comments.Count > 0)
            {
                XmlElement commentNode;
                foreach (string id in comments.AllKeys)
                {
                    //This assumes all inputs are "input" elements with type="text"
                    string nodePath = string.Format("//node()[@name='{0}']", id);
                    commentNode = (XmlElement)transformedData.SelectSingleNode(nodePath);
                    commentNode.InnerText = comments[id];
                }
            }

            return transformedData.DocumentElement.OuterXml;
        }

        public static string RenderAssessmentSubmissionForm(this HtmlHelper helper, Assessment assessment)
        {
            NameValueCollection answers = helper.ViewContext.HttpContext.Request.Form;
            return helper.RenderAssessmentForm(assessment, "~/Content/submit.xsl", answers, null);
        }

        public static string RenderAssessmentFragment(this HtmlHelper helper, string fragment)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(HttpContext.Current.Server.MapPath("~/Content/review.xsl"));

            //Prepare the xml output writer
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            StringWriter sw = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sw, settings);

            //Load the xml document
            XmlDocument assessmentData = new XmlDocument();
            assessmentData.LoadXml(fragment);
            XmlNodeReader reader = new XmlNodeReader(assessmentData);

            //Perform the transform
            xslt.Transform(reader, writer);
            string markup = sw.ToString();

            return markup;
        }
    }
}
