using System;
using System.Collections.Generic;
using Xunit;

namespace CSUtilities.Tests;

public class EnvironmentVarsTests
{
	public static Dictionary<string, object> MockVars { get; set; } = new Dictionary<string, object>();

	static EnvironmentVarsTests()
	{
		MockVars.Add("MOCK_VALUE_STRING", "hello I'm a mock string");
		MockVars.Add("MOCK_VALUE_TRUE", true);
		MockVars.Add("MOCK_VALUE_FALSE", false);
		MockVars.Add("MOCK_VALUE_INT", 10);
		MockVars.Add("MOCK_VALUE_INT_NEG", -550);
		MockVars.Add("MOCK_VALUE_DOUBLE", 2564.3654);
		MockVars.Add("MOCK_VALUE_DOUBLE_NEG", -5550.3654);

		foreach (var item in MockVars)
		{
			Environment.SetEnvironmentVariable(item.Key, item.Value.ToString());
		}
	}

	[Fact]
	public void GetGeneric()
	{
		Assert.Equal("hello I'm a mock string", EnvironmentVars.Get<string>("MOCK_VALUE_STRING", EnvironmentVariableTarget.Process));
		Assert.False(EnvironmentVars.Get<bool>("MOCK_VALUE_FALSE"));
		Assert.True(EnvironmentVars.Get<bool>("MOCK_VALUE_TRUE"));
		Assert.Equal(10, EnvironmentVars.Get<int>("MOCK_VALUE_INT"));
		Assert.Equal(-550, EnvironmentVars.Get<int>("MOCK_VALUE_INT_NEG"));
		Assert.Equal(2564.3654, EnvironmentVars.Get<double>("MOCK_VALUE_DOUBLE"));
		Assert.Equal(-5550.3654, EnvironmentVars.Get<double>("MOCK_VALUE_DOUBLE_NEG"));
	}

	[Fact]
	public void GetTest()
	{
		string mock = EnvironmentVars.Get("MOCK_VALUE_STRING", EnvironmentVariableTarget.Process);
		Assert.Equal("hello I'm a mock string", mock);
	}

	[Fact]
	public void SetIfNull()
	{
		string varname = "TEST_SET";
		string varvalue = "MY_VALUE";

		EnvironmentVars.SetIfNull(varname, varvalue, EnvironmentVariableTarget.Process);
		Assert.Equal(varvalue, Environment.GetEnvironmentVariable(varname));

		EnvironmentVars.SetIfNull(varname, "NEW_VALUE", EnvironmentVariableTarget.Process);
		Assert.Equal(varvalue, Environment.GetEnvironmentVariable(varname));
	}

	[Fact]
	public void SetTest()
	{
		string varname = "TEST_SET";
		string varvalue = "MY_VALUE";
		EnvironmentVars.Set(varname, varvalue, EnvironmentVariableTarget.Process);
		Assert.Equal(varvalue, Environment.GetEnvironmentVariable(varname));
	}

	[Fact]
	public void Set_WithoutTarget_SetsProcessVariable()
	{
		string varname = "TEST_SET_NO_TARGET";
		string varvalue = "VALUE_NO_TARGET";
		EnvironmentVars.Set(varname, varvalue);
		Assert.Equal(varvalue, Environment.GetEnvironmentVariable(varname, EnvironmentVariableTarget.Process));
	}

	[Fact]
	public void Delete_RemovesVariable()
	{
		string varname = "TEST_DELETE";
		Environment.SetEnvironmentVariable(varname, "TO_BE_DELETED");
		EnvironmentVars.Delete(varname);
		Assert.Null(Environment.GetEnvironmentVariable(varname, EnvironmentVariableTarget.Process));
	}

	[Fact]
	public void Delete_WithTarget_RemovesVariable()
	{
		string varname = "TEST_DELETE_TARGET";
		Environment.SetEnvironmentVariable(varname, "TO_BE_DELETED", EnvironmentVariableTarget.Process);
		EnvironmentVars.Delete(varname, EnvironmentVariableTarget.Process);
		Assert.Null(Environment.GetEnvironmentVariable(varname, EnvironmentVariableTarget.Process));
	}

	[Fact]
	public void GetGeneric_ReturnsDefaultIfNotSet()
	{
		string varname = "NOT_SET_GENERIC";
		Assert.Equal(0, EnvironmentVars.Get<int>(varname));
		Assert.Null(EnvironmentVars.Get<string>(varname));
		Assert.False(EnvironmentVars.Get<bool>(varname));
	}
}