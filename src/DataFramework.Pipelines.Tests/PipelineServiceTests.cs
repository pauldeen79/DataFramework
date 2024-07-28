﻿namespace DataFramework.Pipelines.Tests;

public class PipelineServiceTests : TestBase
{
    public class ProcessResult : PipelineServiceTests
    {
        [Fact]
        public async Task Returns_Invalid_When_Pipeline_Returns_Builder_With_ValidationErrors()
        {
            // Arrange
            var pipeline = Fixture.Freeze<IPipeline<ClassContext>>();
            // note that by doing nothing on the received builder in the builder context, the name will be empty, and this is a required field.
            // thus, we are creating an invalid result 8-)
            pipeline.Process(Arg.Any<ClassContext>(), Arg.Any<CancellationToken>()).Returns(x => Result.Success());
            var sourceModel = CreateModel().Build();
            var settings = new PipelineSettingsBuilder().Build();
            var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);

            // Act
            var result = (await pipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);

            // Assert
            result.Status.Should().Be(ResultStatus.Invalid);
        }

        [Fact]
        public async Task Returns_InnerResult_When_Pipeline_Returns_NonSuccesful_Result()
        {
            // Arrange
            var pipeline = Fixture.Freeze<IPipeline<ClassContext>>();
            pipeline.Process(Arg.Any<ClassContext>(), Arg.Any<CancellationToken>()).Returns(x => Result.Error("Kaboom!"));
            var sourceModel = CreateModel().Build();
            var settings = new PipelineSettingsBuilder().Build();
            var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);

            // Act
            var result = (await pipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);

            // Assert
            result.Status.Should().Be(ResultStatus.Error);
            result.ErrorMessage.Should().Be("Kaboom!");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task Returns_InnerResult_With_Value_When_Pipeline_Returns_Succesful_Result()
        {
            // Arrange
            var pipeline = Fixture.Freeze<IPipeline<ClassContext>>();
            pipeline.Process(Arg.Any<ClassContext>(), Arg.Any<CancellationToken>()).Returns(x =>
            {
                x.ArgAt<ClassContext>(0).Builder.WithName("MyClass").WithNamespace("MyNamespace");
                return Result.Success();
            });
            var sourceModel = CreateModel().Build();
            var settings = new PipelineSettingsBuilder().Build();
            var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);

            // Act
            var result = (await pipeline.Process(context)).ProcessResult(context.Builder, context.Builder.Build);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().NotBeNull();
            result.Value!.Name.Should().Be("MyClass");
            result.Value!.Namespace.Should().Be("MyNamespace");
        }
    }
}
