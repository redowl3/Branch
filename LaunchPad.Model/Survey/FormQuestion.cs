using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIAADataModels.Transfer.Survey
{
	public class FormQuestion
	{
		public string QuestionType { get; set; }
		public int Sort { get; set; }
		public List<FormQuestion> ChildQuestions { get; set; }
		public string QuestionData { get; set; }
	}
}
