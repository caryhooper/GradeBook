using System;
using Xunit;
using GradeBook;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact] //VS finds method with [Fact] next to it, then it runs these pass/fail test methods.
        public void BookCalculatesAnAverageGrade()
        {
            //break up unit test into:
            //arrange
            var book = new InMemoryBook("");
            book.AddGrade(90);
            book.AddGrade(80);
            book.AddGrade(70);
            //act
            var result = book.GetStatistics();
            
            //assert
            
            Assert.Equal(90, result.High, 1);
            Assert.Equal(70, result.Low, 1);
            Assert.Equal(80, result.Average, 1);
            Assert.Equal('B', result.Letter);
        }
    }
}
