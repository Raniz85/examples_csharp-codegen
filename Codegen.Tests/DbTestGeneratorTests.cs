using System.Data;
using System.Linq;
using Codegen.Tests.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace Codegen.Tests;

public class DbTestGeneratorTests
{

    [Fact]
    public void ThatOneFileIsGeneratedForEachDatabaseEngine()
    {
        // Given a source code generator
        var generator = new DbTestGenerator();
        
        // And a source file that should trigger source generation
        var source = @"
        using Codegen;

        namespace User.Repository;

        [DbTest]
        public abstract class UserRepositoryTests {
        }
        ";
        
        // When source code generation is run
        var generatedSources = GeneratorHelpers.RunSourceGenerator(generator, source);
        
        // Then an implementation for each database engine is generated
        generatedSources.Should().ContainKey("Codegen/Codegen.DbTestGenerator/UserRepositoryTests.generated.cs")
            .WhoseValue.Trim().Should().Be(@"
// <auto-generated/>

namespace User.Repository;
public class PostgresUserRepositoryTests(): UserRepositoryTests<PostgresEngine>(new PostgresEngine()) {}
public class MariaDbUserRepositoryTests(): UserRepositoryTests<MariaDbEngine>(new MariaDbEngine()) {}
".Trim());
    }
}