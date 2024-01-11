using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace QuickJson.Tests;

public class JsonSpecs(ITestOutputHelper testOutput)
{
    [Fact]
    public void I_can_parse_JSON_containing_a_null_value()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "null"
        );

        // Assert
        json.Should().BeOfType<JsonNull>();
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_true_value()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "true"
        );

        var value = json.GetBool();

        // Assert
        value.Should().BeTrue();
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_false_value()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "false"
        );

        var value = json.GetBool();

        // Assert
        value.Should().BeFalse();
    }

    [Fact]
    public void I_can_parse_JSON_containing_an_integer_number()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "1234"
        );

        var value = json.GetNumber();

        // Assert
        value.Should().Be(1234);
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_negative_integer_number()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "-1234"
        );

        var value = json.GetNumber();

        // Assert
        value.Should().Be(-1234);
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_floating_point_number()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "1234.56"
        );

        var value = json.GetNumber();

        // Assert
        value.Should().Be(1234.56);
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_negative_floating_point_number()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "-1234.56"
        );

        var value = json.GetNumber();

        // Assert
        value.Should().Be(-1234.56);
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_floating_point_number_in_exponential_notation()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "1234.56e-2"
        );

        var value = json.GetNumber();

        // Assert
        value.Should().Be(12.3456);
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_negative_floating_point_number_in_exponential_notation()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "-1234.56e-2"
        );

        var value = json.GetNumber();

        // Assert
        value.Should().Be(-12.3456);
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_zero_number()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            "0"
        );

        var value = json.GetNumber();

        // Assert
        value.Should().Be(0);
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_string()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            "foo"
            """
        );

        var value = json.GetString();

        // Assert
        value.Should().Be("foo");
    }

    [Fact]
    public void I_can_parse_JSON_containing_an_empty_string()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            ""
            """
        );

        var value = json.GetString();

        // Assert
        value.Should().Be("");
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_string_with_escaped_characters()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            "\tfoo \\ \" \/ \b \f bar\r\n"
            """
        );

        var value = json.GetString();

        // Assert
        value.Should().Be("\tfoo \\ \" / \b \f bar\r\n");
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_string_with_invalid_escaped_characters()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            "foo \x"
            """
        );

        var value = json.GetString();

        // Assert
        value.Should().Be("foo \\x");
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_string_with_an_escaped_unicode_literal()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            "foo\u00f8bar"
            """
        );

        var value = json.GetString();

        // Assert
        value.Should().Be("fooøbar");
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_string_with_an_invalid_escaped_unicode_literal()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            "foo \u123"
            """
        );

        var value = json.GetString();

        // Assert
        value.Should().Be("foo \\u123");
    }

    [Fact]
    public void I_can_parse_JSON_containing_an_array()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            [1, "foo", true]
            """
        );

        var value1 = json.GetChild(0).GetNumber();
        var value2 = json.GetChild(1).GetString();
        var value3 = json.GetChild(2).GetBool();

        // Assert
        value1.Should().Be(1);
        value2.Should().Be("foo");
        value3.Should().BeTrue();
    }

    [Fact]
    public void I_can_parse_JSON_containing_an_object()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            {
                "foo": 1,
                "bar": "zzz",
                "baz": true
            }
            """
        );

        var value1 = json.GetChild("foo").GetNumber();
        var value2 = json.GetChild("bar").GetString();
        var value3 = json.GetChild("baz").GetBool();

        // Assert
        value1.Should().Be(1);
        value2.Should().Be("zzz");
        value3.Should().BeTrue();
    }

    [Fact]
    public void I_can_parse_JSON_containing_a_complex_nested_structure()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """
            {
                "release": {
                    "version": "1.2.3",
                    "isLatest": true,
                    "size": 99999,
                    "files": [
                        {
                            "url": "https://example.com/1",
                            "name": "file1.txt"
                        },
                        {
                            "url": "https://example.com/2",
                            "name": "file2.txt"
                        }
                    ]
                }
            }
            """
        );

        var version = json.GetChild("release").GetChild("version").GetString();

        var isLatest = json.GetChild("release").GetChild("isLatest").GetBool();

        var size = json.GetChild("release").GetChild("size").GetNumber();

        var file1Url = json.GetChild("release")
            .GetChild("files")
            .GetChild(0)
            .GetChild("url")
            .GetString();

        var file1Name = json.GetChild("release")
            .GetChild("files")
            .GetChild(0)
            .GetChild("name")
            .GetString();

        var file2Url = json.GetChild("release")
            .GetChild("files")
            .GetChild(1)
            .GetChild("url")
            .GetString();

        var file2Name = json.GetChild("release")
            .GetChild("files")
            .GetChild(1)
            .GetChild("name")
            .GetString();

        // Assert
        version.Should().Be("1.2.3");
        isLatest.Should().BeTrue();
        size.Should().Be(99999);
        file1Url.Should().Be("https://example.com/1");
        file1Name.Should().Be("file1.txt");
        file2Url.Should().Be("https://example.com/2");
        file2Name.Should().Be("file2.txt");
    }

    [Fact]
    public void I_can_parse_JSON_with_leading_whitespace()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """   {"foo": "bar"}"""
        );

        var value = json.GetChild("foo").GetString();

        // Assert
        value.Should().Be("bar");
    }

    [Fact]
    public void I_can_parse_JSON_with_trailing_whitespace()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """{"foo": "bar"}   """
        );

        var value = json.GetChild("foo").GetString();

        // Assert
        value.Should().Be("bar");
    }

    [Fact]
    public void I_can_parse_JSON_with_leading_and_trailing_whitespace()
    {
        // Act
        var json = Json.Parse(
            // lang=json
            """   {"foo": "bar"}   """
        );

        var value = json.GetChild("foo").GetString();

        // Assert
        value.Should().Be("bar");
    }

    [Fact]
    public void I_can_try_to_parse_malformed_JSON_and_get_an_error()
    {
        // Act & assert
        var ex = Assert.Throws<InvalidOperationException>(() => Json.Parse("[abc}"));

        testOutput.WriteLine(ex.Message);
    }

    [Fact]
    public void I_can_try_to_safely_parse_malformed_JSON_and_get_null()
    {
        // Act
        var json = Json.TryParse("[abc}");

        // Assert
        json.Should().BeNull();
    }

    [Fact]
    public void I_can_try_to_parse_JSON_with_unexpected_trailing_characters_and_get_an_error()
    {
        // Act & assert
        var ex = Assert.Throws<InvalidOperationException>(() => Json.Parse("true_"));

        testOutput.WriteLine(ex.Message);
    }

    [Fact]
    public void I_can_try_to_extract_a_value_of_mismatching_type_and_get_an_error()
    {
        // Arrange
        var json = Json.Parse(
            // lang=json
            "null"
        );

        // Act & assert
        var ex1 = Assert.ThrowsAny<InvalidOperationException>(() => json.GetBool());

        var ex2 = Assert.ThrowsAny<InvalidOperationException>(() => json.GetNumber());

        var ex3 = Assert.ThrowsAny<InvalidOperationException>(() => json.GetString());

        testOutput.WriteLine(ex1.Message);
        testOutput.WriteLine(ex2.Message);
        testOutput.WriteLine(ex3.Message);
    }

    [Fact]
    public void I_can_try_to_safely_extract_a_value_of_mismatching_type_and_get_null()
    {
        // Arrange
        var json = Json.Parse(
            // lang=json
            "null"
        );

        // Act
        var asBool = json.TryGetBool();
        var asNumber = json.TryGetNumber();
        var asString = json.TryGetString();

        // Assert
        asBool.Should().BeNull();
        asNumber.Should().BeNull();
        asString.Should().BeNull();
    }

    [Fact]
    public void I_can_try_to_extract_a_non_existing_child_of_an_array_and_get_an_error()
    {
        // Arrange
        var json = Json.Parse(
            // lang=json
            """
            [1, 2, 3]
            """
        );

        // Act & assert
        var ex1 = Assert.ThrowsAny<InvalidOperationException>(() => json.GetChild(-1));

        var ex2 = Assert.ThrowsAny<InvalidOperationException>(() => json.GetChild(5));

        testOutput.WriteLine(ex1.Message);
        testOutput.WriteLine(ex2.Message);
    }

    [Fact]
    public void I_can_try_to_safely_extract_a_non_existing_child_of_an_array_and_get_null()
    {
        // Arrange
        var json = Json.Parse(
            // lang=json
            """
            [1, 2, 3]
            """
        );

        // Act
        var child1 = json.TryGetChild(-1);
        var child2 = json.TryGetChild(5);

        // Assert
        child1.Should().BeNull();
        child2.Should().BeNull();
    }

    [Fact]
    public void I_can_try_to_extract_a_non_existing_child_of_an_object_and_get_an_error()
    {
        // Arrange
        var json = Json.Parse(
            // lang=json
            """
            {"foo": "bar"}
            """
        );

        // Act & assert
        var ex = Assert.ThrowsAny<InvalidOperationException>(() => json.GetChild("baz"));

        testOutput.WriteLine(ex.Message);
    }

    [Fact]
    public void I_can_try_to_safely_extract_a_non_existing_child_of_an_object_and_get_null()
    {
        // Arrange
        var json = Json.Parse(
            // lang=json
            """
            {"foo": "bar"}
            """
        );

        // Act
        var child = json.TryGetChild("baz");

        // Assert
        child.Should().BeNull();
    }
}
