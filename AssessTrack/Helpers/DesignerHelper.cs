using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using System.Text;

namespace AssessTrack.Helpers
{
    public static class XExtensions
    {
        public static string InnerHtml(this XElement el)
        {
            StringBuilder inner = new StringBuilder();
            foreach (XNode n in el.Nodes())
            {
                inner.Append( n.ToString());
            }
            return inner.ToString();
        }
    }

    public static class DesignerHelper
    {

        private static string textTemplate = @"<li class=""question-data-item text-item {1}"">
				    <h3>Text<span class=""delete-item"">[X]</span></h3>
				    <div>
					    <textarea rows=""5"" cols=""40"" >{0}</textarea>
				    </div>				
			    </li>";
        private static string imageTemplate = @"<li class=""question-data-item image-item {1}"">
				    <h3>Image<span class=""delete-item"">[X]</span></h3>
				    <div>
					    <span>Filename:</span><input type=""text"" class=""imgfilename"" value=""{0}""/>
				    </div>				
			    </li>";
        private static string codeTemplate = @"<li class=""question-data-item code-item {1}"">
				    <h3>Code<span class=""delete-item"">[X]</span></h3>
				    <div>
					    <textarea rows=""5"" cols=""40"">{0}</textarea>
				    </div>				
			    </li>";
        private static string answerTemplate = @"<li class=""question-data-item {0} answer {8}"" answer-type=""{6}"" id=""{7}"">
				    <h3>{1}<span class=""delete-item"">[X]</span></h3>
                    {5}
                    <div class='caption meta'><span>Caption:</span><input type='text' value='{2}'/></div>
                    <div class='weight meta'><span>Weight:</span><input type='text' value='{3}'/></div>
                    <div class='tags meta'><span>Tags:</span><input type='text' value='{4}'/></div>
                    <div class='keys meta'><span>Answer Keys</span><ul class=""answerkeys"">{9}</ul><span class='add-key'>[+]</span></div>
			    </li>";
        private static string answerKeyTemplate = @"<li class=""{2}""><span class=""delete-key"">[x]</span>
                                        <div><span>Answer:</span>
                                        <textarea class=""key-answer"">{0}</textarea></div>
                                        <div><span>Point Value:</span><input type=""text"" class=""key-weight"" value=""{1}""/></div>
                                                </li>";
        private static string multichoiceTemplate = @"<div>
					    <span>Choices:</span><textarea rows=""5"" cols=""40"">{0}</textarea>
				    </div>";
        private static string questionTemplate = @"<li class=""question {2}"" id=""{1}"">
				    <h2><span class=""question-number"">New Question</span> <span class=""delete-item"">[X]</span></h2>
				    <ul class=""question-data"">
					    {0}
				    </ul>	
                    <div class='tags meta'><span>Tags:</span><input type='text' value='{3}'/></div>			
			    </li>";
        public static string GetAnswerKeyMarkup(string answer, string weight)
        {
            return string.Format(answerKeyTemplate, answer, weight, "answerkey");
        }

        private static string GetAnswerKeyMenuItem()
        {
            return string.Format(answerKeyTemplate, string.Empty, string.Empty, "tool-box-item answerkey-template");
        }

        public static string GetTextMarkup(string text)
        {
            return GetTextMarkup(text, string.Empty);
        }

        public static string GetTextMarkup(string text, string extraClasses)
        {
            return string.Format(textTemplate, text, extraClasses);
        }

        public static string GetImageMarkup(string image)
        {
            return GetImageMarkup(image, string.Empty);
        }

        public static string GetImageMarkup(string image, string extraClasses)
        {
            return string.Format(imageTemplate, image, extraClasses);
        }

        public static string GetCodeMarkup(string code)
        {
            return GetCodeMarkup(code, string.Empty);
        }

        public static string GetCodeMarkup(string code, string extraClasses)
        {
            return string.Format(codeTemplate, code, extraClasses);
        }

        public static string GetShortAnswerMarkup(string caption, string weight, string tags, string id)
        {
            return GetShortAnswerMarkup(caption, weight, tags, id, string.Empty);
        }

        public static string GetShortAnswerMarkup(string caption, string weight, string tags, string id, string extraClasses)
        {
            return GetShortAnswerMarkup(caption, weight, tags, id, extraClasses, string.Empty);
        }

        public static string GetShortAnswerMarkup(string caption, string weight, string tags, string id, string extraClasses, string keys)
        {
            return string.Format(answerTemplate, "short-answer", "Short Answer",
                caption, weight, tags, string.Empty, "short-answer", id, extraClasses, keys);
        }

        public static string GetLongAnswerMarkup(string caption, string weight, string tags, string id)
        {
            return GetLongAnswerMarkup(caption, weight, tags, id, string.Empty);
        }

        public static string GetLongAnswerMarkup(string caption, string weight, string tags, string id, string extraClasses)
        {
            return string.Format(answerTemplate, "long-answer", "Long Answer",
                caption, weight, tags, string.Empty, "long-answer", id, extraClasses, string.Empty);
        }

        public static string GetLongAnswerMarkup(string caption, string weight, string tags, string id, string extraClasses, string keys)
        {
            return string.Format(answerTemplate, "long-answer", "Long Answer",
                caption, weight, tags, string.Empty, "long-answer", id, extraClasses, keys);
        }


        public static string GetCodeAnswerMarkup(string caption, string weight, string tags, string id)
        {
            return GetCodeAnswerMarkup(caption, weight, tags, id, string.Empty);
        }

        public static string GetCodeAnswerMarkup(string caption, string weight, string tags, string id, string extraClasses)
        {
            return string.Format(answerTemplate, "code-answer", "Code Answer",
                caption, weight, tags, string.Empty, "code-answer", id, extraClasses, string.Empty);
        }

        public static string GetCodeAnswerMarkup(string caption, string weight, string tags, string id, string extraClasses, string keys)
        {
            return string.Format(answerTemplate, "code-answer", "Code Answer",
                caption, weight, tags, string.Empty, "code-answer", id, extraClasses, keys);
        }

        public static string GetMultichoiceMarkup(string caption, string weight, string tags, string id, string choices)
        {
            return GetMultichoiceMarkup(caption, weight, tags, id, choices, string.Empty);
        }

        public static string GetMultichoiceMarkup(string caption, string weight, string tags, string id, string choices, string extraClasses)
        {
            return string.Format(answerTemplate, "multichoice", "Multi Choice",
                caption, weight, tags, string.Format(multichoiceTemplate, choices), "multichoice", id, extraClasses, string.Empty);
        }

        public static string GetMultichoiceMarkup(string caption, string weight, string tags, string id, string choices, string extraClasses, string keys)
        {
            return string.Format(answerTemplate, "multichoice", "Multi Choice",
                caption, weight, tags, string.Format(multichoiceTemplate, choices), "multichoice", id, extraClasses, keys);
        }

        public static string GetQuestionMarkup(string data, string id)
        {
            return GetQuestionMarkup(data, id, string.Empty, string.Empty);
        }

        public static string GetQuestionMarkup(string data, string id, string extraClasses, string tags)
        {
            return string.Format(questionTemplate, data, id, extraClasses, tags);
        }

        //public static string LoadAssessment(int id)
        //{
        //    ExamData exam = new ExamData(id);
        //    return LoadAssessment(exam.Markup);
        //}

        //public static string GetQuestionMarkupFromLegacyXml(XElement question)
        //{
        //    string questiondata = "";
        //    XElement dataElement, answerElement;
        //    string caption, weight, tags, choices;
        //    foreach (XNode dataitem in question.Nodes())
        //    {
        //        if (dataitem is XText)
        //        {
        //            questiondata += GetTextMarkup((dataitem as XText).Value);
        //        }
        //        else if (dataitem is XElement)
        //        {
        //            dataElement = dataitem as XElement;
        //            if (dataElement.Name == "image")
        //            {
        //                questiondata += GetImageMarkup(dataElement.Value);
        //            }
        //            else if (dataElement.Name == "p" || dataElement.Name == "text")
        //            {
        //                questiondata += GetTextMarkup(dataElement.InnerHtml());
        //            }
        //            else if (dataElement.Name == "code")
        //            {
        //                questiondata += GetCodeMarkup(dataElement.Value);
        //            }
        //            else if (dataElement.Name == "answer")
        //            {
        //                //Get the weight attribute (required)
        //                weight = dataElement.Attribute("weight").Value;

        //                //Get the objective and key (optional)
        //                tags = (dataElement.Attribute("objective") != null) ? dataElement.Attribute("objective").Value : "";
        //                //key = (dataElement.Attribute("key") != null) ? dataElement.Attribute("key").Value : "";

        //                //Get the caption
        //                answerElement = dataElement.Element("caption");
        //                caption = (answerElement != null) ? answerElement.Value : "";

        //                //Get the specific answer type
        //                if (answerElement != null)
        //                    answerElement = answerElement.NextNode as XElement;
        //                else
        //                    answerElement = dataElement.FirstNode as XElement;
        //                if (answerElement.Name == "short-answer")
        //                {
        //                    questiondata += GetShortAnswerMarkup(caption, weight, key, tags);
        //                }
        //                else if (answerElement.Name == "long-answer")
        //                {
        //                    questiondata += GetLongAnswerMarkup(caption, weight, key, tags);
        //                }
        //                else if (answerElement.Name == "multichoice")
        //                {
        //                    choices = "";
        //                    foreach (XElement choice in answerElement.Elements("choice"))
        //                    {
        //                        choices += choice.Value + "\n";
        //                    }
        //                    questiondata += GetMultichoiceMarkup(caption, weight, key, tags, choices);
        //                }

        //            }
        //            else if (dataElement.Name == "br")
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                questiondata += GetTextMarkup(dataElement.ToString());
        //            }
        //        }
        //    }
        //    return GetQuestionMarkup(questiondata);
        //}

        public static string GetQuestionMarkupFromXml(XElement question)
        {
            string questiondata = "";
            XElement dataElement, keyElement;
            string caption, weight, tags, choices, type, id, keys;
            string questionid = "";
            string questionTags = (question.Attribute("tags") != null) ? question.Attribute("tags").Value : "";
            XAttribute questionIDAttr = question.Attribute("id");
            if (questionIDAttr != null)
            {
                questionid = questionIDAttr.Value;
            }
            foreach (XNode dataitem in question.Nodes())
            {
                if (dataitem is XText)
                {
                    questiondata += GetTextMarkup((dataitem as XText).Value);
                }
                else if (dataitem is XElement)
                {
                    dataElement = dataitem as XElement;
                    if (dataElement.Name == "image")
                    {
                        questiondata += GetImageMarkup(dataElement.InnerHtml());
                    }
                    else if (dataElement.Name == "p" || dataElement.Name == "text")
                    {
                        questiondata += GetTextMarkup(dataElement.InnerHtml());
                    }
                    else if (dataElement.Name == "code")
                    {
                        questiondata += GetCodeMarkup(dataElement.InnerHtml());
                    }
                    else if (dataElement.Name == "answer")
                    {
                        //Get the weight attribute (required)
                        weight = dataElement.Attribute("weight").Value;

                        id = (dataElement.Attribute("id") != null) ? dataElement.Attribute("id").Value : "";

                        //Get the objective and key (optional)
                        tags = (dataElement.Attribute("tags") != null) ? dataElement.Attribute("tags").Value : "";
                        //key = (dataElement.Attribute("key") != null) ? dataElement.Attribute("key").Value : "";

                        //Get the caption
                        caption = (dataElement.Attribute("caption") != null) ? dataElement.Attribute("caption").Value : "";
                        //get the answer keys
                        keys = "";
                        keyElement = dataElement.Element("AnswerKeys");
                        if (keyElement != null)
                        {
                            foreach (XElement anskey in keyElement.Elements("AnswerKey"))
                            {
                                keys += GetAnswerKeyMarkup(anskey.InnerHtml(), anskey.Attribute("weight").Value);
                            }
                        }

                        //Get the answer type
                        type = dataElement.Attribute("type").Value;
                        if (type == "short-answer")
                        {
                            questiondata += GetShortAnswerMarkup(caption, weight, tags, id, string.Empty, keys);
                        }
                        else if (type == "long-answer")
                        {
                            questiondata += GetLongAnswerMarkup(caption, weight, tags, id, string.Empty, keys);
                        }
                        else if (type == "code-answer")
                        {
                            questiondata += GetCodeAnswerMarkup(caption, weight, tags, id, string.Empty, keys);
                        }
                        else if (type == "multichoice")
                        {
                            choices = "";
                            foreach (XElement choice in dataElement.Elements("choice"))
                            {
                                choices += choice.InnerHtml() + "\n";
                            }
                            questiondata += GetMultichoiceMarkup(caption, weight, tags, id, choices, string.Empty, keys);
                        }

                    }
                    else if (dataElement.Name == "br")
                    {
                        continue;
                    }
                    else
                    {
                        questiondata += GetTextMarkup(dataElement.ToString());
                    }
                }
            }
            return GetQuestionMarkup(questiondata, questionid, "", questionTags);
        }

        public static string LoadAssessment(string data)
        {

            XElement markup = XElement.Parse(data);
            string questions = "";
            foreach (XElement question in markup.Elements("question"))
            {

                questions += GetQuestionMarkupFromXml(question);
            }
            return questions;
        }

        public static string PrintQBMenu(this HtmlHelper html)
        {
            return GetQuestionMarkup(string.Empty, string.Empty, "tool-box-item", "") +
                    GetTextMarkup(string.Empty, "tool-box-item") +
                    GetImageMarkup(string.Empty, "tool-box-item") +
                    GetCodeMarkup(string.Empty, "tool-box-item") +
                    GetCodeAnswerMarkup(string.Empty, "0", string.Empty, string.Empty, "tool-box-item") +
                    GetLongAnswerMarkup(string.Empty, "0", string.Empty, string.Empty, "tool-box-item") +
                    GetShortAnswerMarkup(string.Empty, "0", string.Empty, string.Empty, "tool-box-item") +
                    GetMultichoiceMarkup(string.Empty, "0", string.Empty, string.Empty, string.Empty, "tool-box-item") +
                    GetAnswerKeyMenuItem();
        }

        //public string LoadQuestion(int exam, int question)
        //{
        //    ExamData ed = new ExamData(exam);
        //    XElement el = XElement.Parse(ed.Markup);
        //    return GetQuestionMarkupFromXml(el.Elements("question").ToList()[question]);
        //}

    }
}
