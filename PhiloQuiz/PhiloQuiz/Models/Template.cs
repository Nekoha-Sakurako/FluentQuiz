using System.Collections.Generic;

namespace PhiloQuiz
{
    public static class Philo0
    {
        public static (IEnumerable<SingleChoiceQuestion> SingleChoice, IEnumerable<TrueFalseQuestion> TrueFalse) GetDefault()
        {
            var single = new List<SingleChoiceQuestion>
            {
                new SingleChoiceQuestion(1, "Example Quiz 1",
                    new[]{"A.Answer1", "B.Answer2", "C.Answer3(Correct)", "D.Answer4"},"C"),
                new SingleChoiceQuestion(2, "Example Quiz 2",
                    new[]{"A.Answer1", "B.Answer2(Correct)", "C.Answer3", "D.Answer4"},"B"),
                new SingleChoiceQuestion(3, "Example Quiz 3",
                    new[]{"A.Answer1(Correct)", "B.Answer2", "C.Answer3", "D.Answer4"},"A"),
                new SingleChoiceQuestion(4, "Example Quiz 4",
                    new[]{"A.Answer1", "B.Answer2", "C.Answer3", "D.Answer4(Correct)"},"D"),
                new SingleChoiceQuestion(5, "Example Quiz 5",
                    new[]{"A.Answer1", "B.Answer2(Correct)", "C.Answer3", "D.Answer4"},"B"),
                };
            var tf = new List<TrueFalseQuestion>
            {
                new TrueFalseQuestion(1, "TrueFalse Quiz 1(True)", true),
                new TrueFalseQuestion(2, "TrueFalse Quiz 2(False)", false),
                new TrueFalseQuestion(3, "TrueFalse Quiz 3(True)", true),
                new TrueFalseQuestion(4, "TrueFalse Quiz 4(False)", false),
                new TrueFalseQuestion(5, "TrueFalse Quiz 5(True)", true),
                };
            return (single, tf);
        }
    }

    // 移出到命名空间级别，确保在项目其他文件中直接使用 SingleChoiceQuestion / TrueFalseQuestion
    public record SingleChoiceQuestion(int Id, string Question, string[] Options, string Answer);
    public record TrueFalseQuestion(int Id, string Question, bool Answer);
}