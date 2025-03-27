using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorService.Server.Services;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace CalculatorService.Server.Services.Tests
{

	[TestClass()]
	public class CalculatorServiceTests
	{
		private CalculatorService _service;

		[TestInitialize]
		public void Initialize()
		{
			_service = new CalculatorService();
		}

		[TestMethod]
		public void Add_MultipleNumbers_ReturnsCorrectSum()
		{

			var numbers = new double[] { 1, 2, 3, 4 };

			var result = _service.Add(numbers);

			Assert.Equals(10, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Add_LessThanTwoNumbers_ThrowsException()
		{
			var numbers = new double[] { 1 };

			_service.Add(numbers);
		}

		[TestMethod]
		public void Subtract_ValidNumbers_ReturnsCorrectDifference()
		{

			var result = _service.Substract(5, 3);

			Assert.Equals(2, result);
		}

		[TestMethod]
		public void Multiply_ThreeNumbers_ReturnsCorrectProduct()
		{

			var numbers = new double[] { 2, 3, 4 };

			var result = _service.Multiply(numbers);

			Assert.Equals(24, result);
		}

		[TestMethod]
		[ExpectedException(typeof(DivideByZeroException))]
		public void Divide_ByZero_ThrowsException()
		{

			_service.Divide(10, 0);
		}

		[TestMethod]
		public void SquareRoot_PerfectSquare_ReturnsExactValue()
		{

			var result = _service.SquareRoot(16);

			Assert.Equals(4, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SquareRoot_NegativeNumber_ThrowsException()
		{
			_service.SquareRoot(-1);
		}
	}
}