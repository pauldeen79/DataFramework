namespace DataFramework.Pipelines.Tests;

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
            pipeline.ProcessAsync(Arg.Any<ClassContext>(), Arg.Any<CancellationToken>()).Returns(x => Result.Success());
            var sourceModel = CreateModel().Build();
            var settings = new PipelineSettingsBuilder().Build();
            var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);

            // Act
            var result = (await pipeline.ProcessAsync(context)).ProcessResult(context.Builder, context.Builder.Build);

            // Assert
            result.Status.ShouldBe(ResultStatus.Invalid);
        }

        [Fact]
        public async Task Returns_InnerResult_When_Pipeline_Returns_NonSuccesful_Result()
        {
            // Arrange
            var pipeline = Fixture.Freeze<IPipeline<ClassContext>>();
            pipeline.ProcessAsync(Arg.Any<ClassContext>(), Arg.Any<CancellationToken>()).Returns(x => Result.Error("Kaboom!"));
            var sourceModel = CreateModel().Build();
            var settings = new PipelineSettingsBuilder().Build();
            var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);

            // Act
            var result = (await pipeline.ProcessAsync(context)).ProcessResult(context.Builder, context.Builder.Build);

            // Assert
            result.Status.ShouldBe(ResultStatus.Error);
            result.ErrorMessage.ShouldBe("Kaboom!");
            result.Value.ShouldBeNull();
        }

        [Fact]
        public async Task Returns_InnerResult_With_Value_When_Pipeline_Returns_Succesful_Result()
        {
            // Arrange
            var pipeline = Fixture.Freeze<IPipeline<ClassContext>>();
            pipeline.ProcessAsync(Arg.Any<ClassContext>(), Arg.Any<CancellationToken>()).Returns(x =>
            {
                x.ArgAt<ClassContext>(0).Builder.WithName("MyClass").WithNamespace("MyNamespace");
                return Result.Success();
            });
            var sourceModel = CreateModel().Build();
            var settings = new PipelineSettingsBuilder().Build();
            var context = new ClassContext(sourceModel, settings, CultureInfo.InvariantCulture);

            // Act
            var result = (await pipeline.ProcessAsync(context)).ProcessResult(context.Builder, context.Builder.Build);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldNotBeNull();
            result.Value!.Name.ShouldBe("MyClass");
            result.Value!.Namespace.ShouldBe("MyNamespace");
        }
    }
}
