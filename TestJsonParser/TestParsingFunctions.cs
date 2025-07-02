using TechTestAzureFunctions.JsonParsers;

namespace TestJsonParser;

[TestFixture]
public class TestParsingFunctions
{
    private JsonParser _jsonParser;

    private const string JsonInput = """        
        {
            "CustomerId": "68236ef4c8715b4c05c20a55",
            "Property": "email"
        }        
        """;

    private const string JsonInput2 = """        
        {
            "CustomerId": "682370a8c5476986d6e00b3e",
            "Property": "MeetingRequest",
            "date": "2025-05-13T16:17:44.061Z"
        }        
        """; // includes an additional date field to test that it is ignored

    private const string JsonInput3 = """        
        {
            "CustomerId": "682372e7c5476986d6e048b4",
            "Property": "WorkflowTrigger"
        }        
        """;

    private const string JsonInputIdTooShort = """        
        {
            "CustomerId": "682372e7c5476986d6e048b",    
            "Property": "WorkflowTrigger"
        }        
        """;    // CustomerId only 23 characters


    private const string JsonInputIdTooLong = """        
        {
            "CustomerId": "682372e7c5476986d6e048b4d",    
            "Property": "WorkflowTrigger"
        }        
        """;    // CustomerId 25 characters
    [SetUp]
    public void Setup()
    {
        // as the test cases use different JSON inputs for the JsonParser constructor, do the setup within the test methods
    }

    [Test, Description("Verify that the CustomerId value is obtained correctly from a valid input of 24 characters")]
    [TestCase(JsonInput, "68236ef4c8715b4c05c20a55")]
    [TestCase(JsonInput2, "682370a8c5476986d6e00b3e")]
    [TestCase(JsonInput3, "682372e7c5476986d6e048b4")]
    public void TestGetCustomerId_Success(string jsonInput, string expectedValue)
    {
        _jsonParser = new JsonParser(jsonInput);

        var customerId = _jsonParser.GetCustomerId();
        Assert.That(expectedValue, Is.EqualTo(customerId));
        Assert.That(expectedValue.Length, Is.EqualTo(customerId.Length), $"CustomerId value should be {expectedValue.Length} characters");
    }

    [Test, Description("Verify that a null, empty or whitespace CustomerId input throws an ArgumentException")]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void TestGetCustomerId_EmptyInput(string? jsonInput)
    {
        _jsonParser = new JsonParser(jsonInput);

        Assert.Throws<ArgumentException>(() => _jsonParser.GetCustomerId());
    }

    [Test, Description("Verify that a CustomerId value which is not equal to 24 characters throws an ArgumentException")]
    [TestCase(JsonInputIdTooShort)]
    [TestCase(JsonInputIdTooLong)]
    public void TestGetCustomerId_InvalidInput_Fails(string jsonInput)
    {
        _jsonParser = new JsonParser(jsonInput);

        Assert.Throws<ArgumentException>(() => _jsonParser.GetCustomerId());
    }

    [Test, Description("Verify that the Property value is obtained correctly from a valid input")]
    [TestCase(JsonInput, "email")]
    [TestCase(JsonInput2, "MeetingRequest")]
    [TestCase(JsonInput3, "WorkflowTrigger")]
    public void TestGetProperty_Success(string jsonInput, string expectedValue)
    {
        _jsonParser = new JsonParser(jsonInput);

        var propertyValue = _jsonParser.GetProperty();
        Assert.That(expectedValue, Is.EqualTo(propertyValue));
    }

    [Test, Description("Verify that a null, empty or whitespace Property input throws an ArgumentException")]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void TestGetProperty_EmptyInput(string? jsonInput)
    {
        _jsonParser = new JsonParser(jsonInput);

        Assert.Throws<ArgumentException>(() => _jsonParser.GetProperty());
    }
}
