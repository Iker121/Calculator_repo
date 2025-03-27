using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorService.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Server.Services.Tests
{
	[TestClass()]
	public class JournalServiceTests
	{
		private JournalService _journalService;
		[TestInitialize]
		public void Initialize()
		{
			_journalService = new JournalService();
		}

		[TestMethod()]
		public void AddEntry_NewTrackingId_CreatesNewJournal() 
		{
			var trackingId = Guid.NewGuid().ToString();
			var operation = "Add";
			var calculation = "1+2=3";

			_journalService.AddEntry(trackingId, operation, calculation);
			var entries = _journalService.GetEntries(trackingId);

			Assert.AreEqual(1, entries.Count);
			Assert.AreEqual(operation, entries[0].operacion);
			Assert.AreEqual(calculation, entries[0].calculo);
		}

		[TestMethod]
		public void AddEntry_ExistingTrackingId_AddsToExistingJournal()
		{
			var trackingId = Guid.NewGuid().ToString();
			_journalService.AddEntry(trackingId, "Add", "1+2=3");

			_journalService.AddEntry(trackingId, "Subtract", "5-3=2");
			var entries = _journalService.GetEntries(trackingId);

			Assert.AreEqual(2, entries.Count);
			Assert.AreEqual("Subtract", entries[1].operacion);
		}
		[TestMethod]
		public void GetEntries_UnknownTrackingId_ReturnsEmptyList()
		{
			var unknowId = Guid.NewGuid().ToString();

			var entries = _journalService.GetEntries(unknowId);

			Assert.IsNotNull(entries);
			Assert.AreEqual(0, entries.Count);
		}

		[TestMethod]
		public void AddEntry_NullTrackingId_DoesNotThrow()
		{
			string nullId = null;

			_journalService.AddEntry(nullId, "Test", "Test");
		}
		[TestMethod]
		public void GetEntries_MultipleOperations_ReturnsInOrder()
		{
			var trackingId = Guid.NewGuid().ToString();
			_journalService.AddEntry(trackingId, "First", "1+1=2");
			System.Threading.Thread.Sleep(10);

			_journalService.AddEntry(trackingId, "Second", "2+2=4");

			var entries = _journalService.GetEntries(trackingId);

			Assert.AreEqual(2,entries.Count);
			Assert.AreEqual("Second", entries[0].operacion);

			Assert.AreEqual("First", entries[1].operacion);
		}
	}
}