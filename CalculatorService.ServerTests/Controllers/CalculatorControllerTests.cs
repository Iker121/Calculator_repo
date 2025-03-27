﻿using CalculatorServer.Library;
using CalculatorServer.Library.Models;
using CalculatorService.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CalculatorService.Server.Controllers.Tests
{
	[TestClass]
	public class CalculatorControllerTests
	{
		private Mock<ICalculatorService> _calculatorMock;
		private Mock<IJournalService> _journalMock;
		private CalculatorController _controller;

		[TestInitialize]
		public void Initialize()
		{
			_calculatorMock = new Mock<ICalculatorService>();
			_journalMock = new Mock<IJournalService>();
			_controller = new CalculatorController(_calculatorMock.Object, _journalMock.Object);
		}

		[TestMethod]
		public void Constructor_WithDependencies_InitializesCorrectly()
		{
			Assert.IsNotNull(_controller);
		}

		[TestMethod]
		public void Add_ValidRequest_ReturnsOkResult()
		{
			var request = new AddRequest { Sumandos = new double[] { 1, 2 } };
			_calculatorMock.Setup(x => x.Add(request.Sumandos)).Returns(3);
			var result = _controller.Add(request);
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			var okResult = result as OkObjectResult;
			Assert.AreEqual(200, okResult.StatusCode);
			Assert.AreEqual(3, (okResult.Value as AddResponse).Sum);
		}

		[TestMethod]
		public void Add_InvalidRequest_ReturnsBadRequest()
		{
			var request = new AddRequest { Sumandos = new double[] { 1 } };
			_controller.ModelState.AddModelError("Sumandos", "Al menos dos numeros");
			var result = _controller.Add(request);
			Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
		}

		[TestMethod]
		public void Substract_ValidRequest_ReturnsCorrectResult()
		{
			var request = new SubstractRequest { minuendo = 5, substraendo = 3 };
			_calculatorMock.Setup(x => x.Substract(request.minuendo, request.substraendo)).Returns(2);
			var result = _controller.Substract(request);
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			var okResult = result as OkObjectResult;
			Assert.AreEqual(2, (okResult.Value as SubstractResponse).Diferencia);
		}

		[TestMethod]
		public void Multiply_ValidRequest_ReturnsCorrectResult()
		{
			var request = new MultiplyRequest { factors = new double[] { 2, 3 } };
			_calculatorMock.Setup(x => x.Multiply(request.factors)).Returns(6);
			var result = _controller.Multiply(request);
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			var okResult = result as OkObjectResult;
			Assert.AreEqual(6, (okResult.Value as MultiplyResponse).producto);
		}

		[TestMethod]
		public void Divide_ValidRequest_ReturnsCorrectResult()
		{
			var request = new DivideRequest { dividendo = 10, divisor = 3 };
			_calculatorMock.Setup(x => x.Divide(request.dividendo, request.divisor)).Returns((3.333, 1));
			var result = _controller.Divide(request);
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			var okResult = result as OkObjectResult;
			var response = okResult.Value as DivideResponse;
			Assert.AreEqual(3.333, response.cociente, 0.001);
			Assert.AreEqual(1, response.resto);
		}

		[TestMethod]
		public void Divide_ByZero_ReturnsBadRequest()
		{
			var request = new DivideRequest { dividendo = 10, divisor = 0 };
			_calculatorMock.Setup(x => x.Divide(request.dividendo, request.divisor)).Throws(new DivideByZeroException());
			var result = _controller.Divide(request);
			Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
		}

		[TestMethod]
		public void SquareRoot_ValidRequest_ReturnsCorrectResult()
		{
			var request = new SquareRootRequeste { numero = 16 };
			_calculatorMock.Setup(x => x.SquareRoot(request.numero)).Returns(4);
			var result = _controller.SquareRoot(request);
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			var okResult = result as OkObjectResult;
			Assert.AreEqual(4, (okResult.Value as SquareRootResponse).cuadrado);
		}

		[TestMethod]
		public void JournalQuery_ValidRequest_ReturnsEntries()
		{
			var request = new JournalQueryRequest { Id = "test123" };
			var entries = new List<JournalEntry> { new JournalEntry { operacion = "Add", calculo = "1+2=3", Date = DateTime.Now } };
			_journalMock.Setup(x => x.GetEntries(request.Id)).Returns(entries);
			var result = _controller.JournalQuery(request);
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			var okResult = result as OkObjectResult;
			Assert.AreEqual(1, (okResult.Value as JournalQueryResponse).operaciones.Count);
		}

		[TestMethod]
		public void JournalQuery_InvalidRequest_ReturnsBadRequest()
		{
			var request = new JournalQueryRequest { Id = "" };
			var result = _controller.JournalQuery(request);
			Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
		}

		[TestMethod]
		public void Add_WithTrackingId_LogsJournalEntry()
		{
			var request = new AddRequest { Sumandos = new double[] { 1, 2 } };
			const string trackingId = "track-123";
			_calculatorMock.Setup(x => x.Add(request.Sumandos)).Returns(3);
			var result = _controller.Add(request, trackingId);
			_journalMock.Verify(x => x.AddEntry(trackingId, "Add", "1+2 = 3"), Times.Once);
		}
	}
}