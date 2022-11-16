using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using csharp;
using NUnit.Framework;

namespace Tests
{
    [UseReporter(typeof(DiffReporter))]
    [TestFixture]
    public class ApprovalTest
    {
        private StringBuilder _builder;
        private StringWriter _writer;
        private StringReader _reader;

        [SetUp]
        public void SetUp()
        {
            _builder = new StringBuilder();
            _writer = new StringWriter(_builder);
            _reader = new StringReader("a\n");

            Console.SetOut(_writer);
            Console.SetIn(_reader);
        }

        [Test]
        public void ValidateOutput_After_30Days()
        {
            Program.Main(new string[] { });
            
            Approvals.Verify(_builder.ToString());
        }
    }
}
