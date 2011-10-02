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
using AssessTrack.Models;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace AssessTrack.Helpers
{
    public static class CodeCompiler
    {
        public static void CompileCodeQuestions(this SubmissionRecord record)
        {
            string VSVarsPath = Environment.GetEnvironmentVariable("VS100COMNTOOLS");
            string vcvars = @"""{0}vsvars32""";
            string cl = "cl {0} /EHsc";
            string ExeArgs = @"/c ""{0}vsvars32"" & cl {1} /EHsc";
            string failMsg = "\nCompile failed for Question #{0}, Answer #{1} on {2}.\n Compiler Output:\n{3}\n\n************************************************************\n\n";
            string longrunMsg = "\nExecution failed for Question #{0}, Answer #{1} on {2}.\n Program did not run in allotted time span.\n\n************************************************************\n\n";
            string successMessage = "\nCompile and Execute succeeded for Question #{0}, Answer #{1} on {2}.\nOutput: \n{3}\n\n************************************************************\n\n";
            
            //Create working directory
            string tempDir = Environment.GetEnvironmentVariable("TEMP") + "\\cms-compiles\\" + record.SubmissionRecordID.ToString();
            Directory.CreateDirectory(tempDir);

            foreach (Response response in record.Responses)
            {
                if (response.Answer.Type == "code-answer")
                {
                    string source = response.ResponseID.ToString() + ".cpp";
                    string args = string.Format(ExeArgs, VSVarsPath, source);
                    System.IO.File.WriteAllText(tempDir + "\\" + source, response.ResponseText);
                    ProcessStartInfo startInfo = new ProcessStartInfo("cmd");
                    startInfo.RedirectStandardInput = true;
                    startInfo.WorkingDirectory = tempDir;
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    Process compiler = new Process();
                    compiler.StartInfo = startInfo;
                    compiler.Start();
                    compiler.StandardInput.WriteLine(string.Format(vcvars, VSVarsPath));
                    compiler.StandardInput.WriteLine(string.Format(cl, source));
                    compiler.StandardInput.Flush();
                    compiler.StandardInput.Close();
                    
                    compiler.WaitForExit(5000); //Wait 5 seconds for compiler to finish
                    //Just to be safe, KILL it
                    //If it's not done yet then something dumb happened (probably)
                    string compilerOutput = compiler.StandardOutput.ReadToEnd() + "\n" + compiler.StandardError.ReadToEnd();
                    compiler.StandardOutput.Close();
                    compiler.StandardError.Close();
                    if (!compiler.HasExited)
                        compiler.Kill();
                    

                    string exe = tempDir + @"\" + response.ResponseID.ToString() + ".exe";
                    if (!System.IO.File.Exists(exe))
                    {
                        response.Comment = string.Format(failMsg, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString(), compilerOutput);
                        //record.Comments += string.Format(failMsg, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString(), compilerOutput);
                        continue;
                    }

                    //Run program
                    ProcessStartInfo userProgramInfo = new ProcessStartInfo(exe);
                    userProgramInfo.WorkingDirectory = tempDir;
                    userProgramInfo.UseShellExecute = false;
                    userProgramInfo.RedirectStandardOutput = true;
                    userProgramInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    userProgramInfo.CreateNoWindow = true;
                    if (!string.IsNullOrEmpty(response.Answer.Stdin))
                    {
                        userProgramInfo.RedirectStandardInput = true;
                    }
                    
                    Process userProgram = new Process();
                    userProgram.StartInfo = userProgramInfo;
                    //Write out input file if it exists
                    if (!string.IsNullOrEmpty(response.Answer.Fstream))
                    {
                        System.IO.File.WriteAllText(tempDir + "\\infile.txt", response.Answer.Fstream);
                        System.IO.File.WriteAllText(tempDir + "\\indata.txt", response.Answer.Fstream);
                    }
                    userProgram.Start();
                    if (!string.IsNullOrEmpty(response.Answer.Stdin))
                    {
                        userProgram.StandardInput.Write(response.Answer.Stdin);
                        userProgram.StandardInput.Flush();
                        userProgram.StandardInput.Close();
                    }
                    int seconds = 0;
                    while (seconds < 30 && !userProgram.HasExited)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                    }
                    if (seconds == 30) //Program failed to run allotted time. KILL IT
                    {
                        userProgram.Kill();
                        response.Comment = string.Format(longrunMsg, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString());
                        //record.Comments += string.Format(longrunMsg, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString());
                        continue;
                    }
                    //Get output and set comments
                    string output = userProgram.StandardOutput.ReadToEnd();
                    response.Comment = string.Format(successMessage, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString(), output);
                    //record.Comments += string.Format(successMessage, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString(),output);
                }
            }

            //Delete Temp Folder
            Directory.Delete(tempDir, true);
        }
    }
}
