using System;
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

            NameValueCollection scores;
            if (helper.ViewContext.HttpContext.Request.Form.Count > 0)
            {
                scores = helper.ViewContext.HttpContext.Request.Form;
            }
            else
            {
                scores = new NameValueCollection();
                foreach (Response response in submission.Responses)
                {
                    scores.Add(response.AnswerID.ToString(), (response.Score ?? 0.0).ToString());
                }
            }

            return helper.RenderAssessmentForm(submission.Assessment, "~/Content/grade.xsl", answers, scores);

        }

        public static string RenderAssessmentForm(this HtmlHelper helper, Assessment assessment, string stylesheet, NameValueCollection answers, NameValueCollection inputs)
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
                    if (answer.Type == "short-answer")
                    {
                        answerNode = transformedData.SelectSingleNode(String.Format("//input[@id='{0}']", answer.AnswerID.ToString()));
                        XmlAttribute value = transformedData.CreateAttribute("value");
                        value.Value = answers[answer.AnswerID.ToString()];
                        answerNode.Attributes.Append(value);
                    }
                    else if (answer.Type == "long-answer" || answer.Type == "code-answer")
                    {
                        answerNode = transformedData.SelectSingleNode(String.Format("//textarea[@id='{0}']", answer.AnswerID.ToString()));
                        answerNode.InnerText = answers[answer.AnswerID.ToString()];
                    }
                    else if (answer.Type == "multichoice")
                    {
                        answerNode = transformedData.SelectSingleNode(String.Format("//input[starts-with(@id,'{0}')][@value='{1}']", answer.AnswerID.ToString(), answers[answer.AnswerID.ToString()]));
                        XmlAttribute checkedAttr = transformedData.CreateAttribute("checked");
                        checkedAttr.Value = "true";
                        answerNode.Attributes.Append(checkedAttr);
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

            
            return transformedData.DocumentElement.OuterXml;
        }

        public static string RenderAssessmentSubmissionForm(this HtmlHelper helper, Assessment assessment)
        {
            NameValueCollection answers = helper.ViewContext.HttpContext.Request.Form;
            return helper.RenderAssessmentForm(assessment, "~/Content/submit.xsl", answers, null);
        }
    }
}
