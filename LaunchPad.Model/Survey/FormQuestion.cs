using System;
using System.Collections.Generic;

namespace IIAADataModels.Transfer.Survey
{
	public class FormQuestion
	{
		public string QuestionType { get; set; }
		public int Sort { get; set; }
		public List<FormQuestion> ChildQuestions { get; set; }
		public string QuestionData { get; set; }
		public List<Config> Config { get; set; }
	}
	public class Config
	{
		public string NiceName { get; set; }
		public string Value { get; set; }
	}
}
