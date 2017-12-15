using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kr.ac.kaist.swrc.jhannanum.hannanum;
using kr.ac.kaist.swrc.jhannanum.comm;

namespace redesigned_chatbot
{
    public static class Parser
    {
        static string[] Splitter;
        static Workflow workflow;

        static Parser()
        {
            Splitter = new string[] {
                " ", "은", "는", "를", "이", "가", "나", "어", "지", "고", "거", "아", "내", "있", "요", "네", "하", "로", "을지", "답니다", "도", "에", "ㅂ니다", "는데요", "었는데" ,"의" ,"이르", "어는", "게" ,"어다" ,"니" ,"에게"
            };
            workflow = WorkflowFactory.getPredefinedWorkflow(WorkflowFactory.WORKFLOW_POS_SIMPLE_09);
            workflow.activateWorkflow(true);
        }

        public static string[] ParseKorean(string input)
        {
            if (string.IsNullOrEmpty(input)) return new string[] { };
  
            workflow.analyze(input);
            var result = workflow.getResultOfDocument(new Sentence(0, 0, false));
            
            var res = from sentence in result
                      from eojeol in sentence.Eojeols
                      from morpheme in eojeol.Morphemes
                      where !Splitter.Contains(morpheme)
                      select morpheme;

            return res.ToArray();
        }
    }
}
