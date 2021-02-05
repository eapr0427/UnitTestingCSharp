using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Push_ArgIsNullObject_ThrowArgumentNullException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ArgIsValidObject_AddObjectToTheStack()
        {
            var stack = new Stack<string>();
            stack.Push("FJPS");
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack<string>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }


        [Test]
        public void Pop_StackIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackIsNotEmpty_ReturnObjectOnTopOfTheStack()
        {
            //Arrange
            var stack = new Stack<string>();

            stack.Push("FJPS");
            stack.Push("JOVI");
            stack.Push("MIA");

            //Act
            var result = stack.Pop();

            //Assert
            Assert.That(result, Is.EqualTo("MIA"));
        }

        [Test]
        public void Pop_StackIsNotEmpty_RemoveObjectOnTopOfTheStack()
        {
            //Arrange
            var stack = new Stack<string>();

            stack.Push("FJPS");
            stack.Push("JOVI");
            stack.Push("MIA");

            //Act
           stack.Pop();

            //Assert
            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_ListIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackIsNotEmpty_ObjectOnTopOfTheStackIsReturned()
        {
            //Arrange
            var stack = new Stack<string>();
            stack.Push("FJPS");
            stack.Push("JOVI");
            stack.Push("MIA");

            //Act

            var result = stack.Peek();

            //Assert
            Assert.That(result, Is.EqualTo("MIA"));
        }

        [Test]
        public void Peek_StackIsNotEmpty_ObjectOnTopOfTheStackIsNotRemoved()
        {
            //Arrange
            var stack = new Stack<string>();

            stack.Push("FJPS");
            stack.Push("JOVI");
            stack.Push("MIA");

            //Act
            stack.Peek();

            //Assert
            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}
