using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace QuickJson.Tests
{
    public class MainSpecs
    {
        private readonly ITestOutputHelper _output;

        public MainSpecs(ITestOutputHelper output) => _output = output;

        [Fact]
        public void User_can_parse_JSON_containing_a_null_node()
        {
            // Act
            var json = Json.Parse("null");

            // Assert
            json.Should().BeOfType<JsonNull>();
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_true_bool_node()
        {
            // Act
            var json = Json.Parse("true");
            var value = json.TryGetBool();

            // Assert
            value.Should().NotBeNull();
            value?.Should().BeTrue();
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_false_bool_node()
        {
            // Act
            var json = Json.Parse("false");
            var value = json.TryGetBool();

            // Assert
            value.Should().NotBeNull();
            value.Should().BeFalse();
        }

        [Fact]
        public void User_can_parse_JSON_containing_an_integer_number_node()
        {
            // Act
            var json = Json.Parse("1234");
            var value = json.TryGetNumber();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be(1234);
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_floating_point_number_node()
        {
            // Act
            var json = Json.Parse("1234.56");
            var value = json.TryGetNumber();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be(1234.56);
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_string_node()
        {
            // Act
            var json = Json.Parse("\"foo\"");
            var value = json.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("foo");
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_string_node_of_zero_length()
        {
            // Act
            var json = Json.Parse("\"\"");
            var value = json.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("");
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_string_node_with_escaped_characters()
        {
            // Act
            var json = Json.Parse("\"\\tfoo \\\\ \\\" \\/ \\b \\f bar\\r\\n\"");
            var value = json.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("\tfoo \\ \" / \b \f bar\r\n");
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_string_node_with_invalid_escaped_characters()
        {
            // Act
            var json = Json.Parse("\"foo \\x\"");
            var value = json.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("foo \\x");
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_string_node_with_an_escaped_unicode_literal()
        {
            // Act
            var json = Json.Parse("\"foo\\u00f8bar\"");
            var value = json.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("fooøbar");
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_string_node_with_an_invalid_escaped_unicode_literal()
        {
            // Act
            var json = Json.Parse("\"foo \\u123\"");
            var value = json.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("foo \\u123");
        }

        [Fact]
        public void User_can_parse_JSON_containing_an_array_node()
        {
            // Act
            var json = Json.Parse("[1, \"foo\", true]");

            var value1 = json.TryGetChild(0)?.TryGetNumber();
            var value2 = json.TryGetChild(1)?.TryGetString();
            var value3 = json.TryGetChild(2)?.TryGetBool();

            // Assert
            value1.Should().NotBeNull();
            value1?.Should().Be(1);

            value2.Should().NotBeNull();
            value2?.Should().Be("foo");

            value3.Should().NotBeNull();
            value3?.Should().BeTrue();
        }

        [Fact]
        public void User_can_parse_JSON_containing_an_object_node()
        {
            // Act
            var json = Json.Parse("{\"foo\": 1, \"bar\": \"zzz\", \"baz\": true}");

            var value1 = json.TryGetChild("foo")?.TryGetNumber();
            var value2 = json.TryGetChild("bar")?.TryGetString();
            var value3 = json.TryGetChild("baz")?.TryGetBool();

            // Assert
            value1.Should().NotBeNull();
            value1?.Should().Be(1);

            value2.Should().NotBeNull();
            value2?.Should().Be("zzz");

            value3.Should().NotBeNull();
            value3?.Should().BeTrue();
        }

        [Fact]
        public void User_can_parse_JSON_containing_a_complex_structure()
        {
            // Act
            var json = Json.Parse(@"
                {
                    ""release"": {
                        ""version"": ""1.2.3"",
                        ""isLatest"": true,
                        ""size"": 99999,
                        ""files"": [
                            {
                                ""url"": ""https://example.com/1"",
                                ""name"": ""file1.txt""
                            },
                            {
                                ""url"": ""https://example.com/2"",
                                ""name"": ""file2.txt""
                            }
                        ]
                    }
                }
            ");

            var version = json
                .TryGetChild("release")?
                .TryGetChild("version")?
                .TryGetString();

            var isLatest = json
                .TryGetChild("release")?
                .TryGetChild("isLatest")?
                .TryGetBool();

            var size = json
                .TryGetChild("release")?
                .TryGetChild("size")?
                .TryGetNumber();

            var file1Url = json
                .TryGetChild("release")?
                .TryGetChild("files")?
                .TryGetChild(0)?
                .TryGetChild("url")?
                .TryGetString();

            var file1Name = json
                .TryGetChild("release")?
                .TryGetChild("files")?
                .TryGetChild(0)?
                .TryGetChild("name")?
                .TryGetString();

            var file2Url = json
                .TryGetChild("release")?
                .TryGetChild("files")?
                .TryGetChild(1)?
                .TryGetChild("url")?
                .TryGetString();

            var file2Name = json
                .TryGetChild("release")?
                .TryGetChild("files")?
                .TryGetChild(1)?
                .TryGetChild("name")?
                .TryGetString();

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
        public void User_can_parse_JSON_with_leading_whitespace()
        {
            // Act
            var json = Json.Parse("   {\"foo\": \"bar\"}");
            var value = json.TryGetChild("foo")?.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("bar");
        }

        [Fact]
        public void User_can_parse_JSON_with_trailing_whitespace()
        {
            // Act
            var json = Json.Parse("{\"foo\": \"bar\"}   ");
            var value = json.TryGetChild("foo")?.TryGetString();

            // Assert
            value.Should().NotBeNull();
            value?.Should().Be("bar");
        }

        [Fact]
        public void User_can_try_to_parse_invalid_JSON_and_it_fails()
        {
            // Act & assert
            var ex = Assert.Throws<InvalidOperationException>(() => Json.Parse("[abc}"));
            _output.WriteLine(ex.Message);
        }

        [Fact]
        public void User_can_try_to_parse_invalid_JSON_safely_and_it_returns_null()
        {
            // Act
            var json = Json.TryParse("[abc}");

            // Assert
            json.Should().BeNull();
        }

        [Fact]
        public void User_can_try_to_parse_JSON_with_unexpected_trailing_characters_and_it_fails()
        {
            // Act & assert
            var ex = Assert.Throws<InvalidOperationException>(() => Json.Parse("true_"));
            _output.WriteLine(ex.Message);
        }

        [Fact]
        public void User_can_try_to_safely_extract_an_invalid_value_from_a_node_and_it_returns_null()
        {
            // Arrange
            var json = Json.Parse("null");

            // Act
            var asBool = json.TryGetBool();
            var asNumber = json.TryGetNumber();
            var asString = json.TryGetString();

            // Assert
            asBool.Should().BeNull();
            asNumber.Should().BeNull();
            asString.Should().BeNull();
        }
    }
}