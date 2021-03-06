﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentCassandra.Types;
using System.Threading;

namespace FluentCassandra.Test.TypesToDatabase
{
	[TestClass]
	public class TimeUUIDTypeTest
	{
		public const string FamilyName = "StandardTimeUUIDType";
		public const string TestKey = "Test1";
		private CassandraContext _db;

		[TestInitialize]
		public void TestInit()
		{
			var setup = new _CassandraSetup();
			_db = setup.DB;
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_db.Dispose();
		}

		[TestMethod]
		public void Save_Guid()
		{
			// arrange
			var family = _db.GetColumnFamily<TimeUUIDType>(FamilyName);
			Guid expected = new Guid("38400000-8cf0-11bd-b23e-10b96e4ef00d");

			// act
			family.InsertColumn(TestKey, expected, Math.PI);
			var actual = family.GetColumn(TestKey, expected);

			// assert
			Assert.AreEqual<Guid>(expected, actual.ColumnName);
		}

		[TestMethod]
		public void Save_DateTime_Local()
		{
			// arrange
			var family = _db.GetColumnFamily<TimeUUIDType>(FamilyName);
			var expected = DateTime.Now;

			// act
			family.InsertColumn(TestKey, expected, Math.PI);
			var actualColumn = family.GetColumn(TestKey, expected);
			var actual = ((DateTime)actualColumn.ColumnName).ToLocalTime();

			// assert
			Assert.AreEqual<DateTime>(expected, actual);
		}

		[TestMethod]
		public void Save_DateTime_Universal()
		{
			// arrange
			var family = _db.GetColumnFamily<TimeUUIDType>(FamilyName);
			var expected = DateTime.UtcNow;

			// act
			family.InsertColumn(TestKey, expected, Math.PI);
			var actual = family.GetColumn(TestKey, expected);

			// assert
			Assert.AreEqual<DateTime>(expected, actual.ColumnName);
		}

		[TestMethod]
		public void Save_DateTimeOffset()
		{
			// arrange
			var family = _db.GetColumnFamily<TimeUUIDType>(FamilyName);
			var expected = DateTimeOffset.Now;

			// act
			family.InsertColumn(TestKey, expected, Math.PI);
			var actual = family.GetColumn(TestKey, expected);

			// assert
			Assert.AreEqual<DateTimeOffset>(expected, actual.ColumnName);
		}
	}
}
