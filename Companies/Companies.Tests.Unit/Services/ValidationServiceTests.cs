namespace Companies.Tests.Unit.Services;

public class ValidationServiceTests
{
    private record TestRecord;
    
    private readonly ILogger _logger = new Mock<ILogger>().Object;
    private readonly Mock<IValidator<TestRecord>> _firstValidator = new();
    private readonly Mock<IValidator<TestRecord>> _secondValidator = new();
    private readonly ValidationService<TestRecord> _service;

    public ValidationServiceTests()
    {
        _service = new ValidationService<TestRecord>([_firstValidator.Object, _secondValidator.Object], _logger);
    }

    [Fact]
    public async Task ShouldReturnOkWhenAllValidatorsPass()
    {
        var record = new TestRecord();

        _firstValidator.Setup(x => x.ValidateOnSave(record)).ReturnsAsync(new Result(true));
        _secondValidator.Setup(x => x.ValidateOnSave(record)).ReturnsAsync(new Result(true));

        var result = await _service.ValidateOnSave(record);

        Assert.True(result.Success);
        _firstValidator.Verify(x => x.ValidateOnSave(record), Times.Once);
        _secondValidator.Verify(x => x.ValidateOnSave(record), Times.Once);
    }


    [Fact]
    public async Task ShouldFailOnFirstValidation()
    {
        var record = new TestRecord();

        _firstValidator.Setup(x => x.ValidateOnSave(record)).ReturnsAsync(new Result(false, "Bad record"));
        _secondValidator.Setup(x => x.ValidateOnSave(record)).ReturnsAsync(new Result(true));

        var result = await _service.ValidateOnSave(record);

        Assert.False(result.Success);
        Assert.Equal("Bad record", result.ErrorMessage);
        _firstValidator.Verify(x => x.ValidateOnSave(record), Times.Once);
        _secondValidator.Verify(x => x.ValidateOnSave(record), Times.Never);
    }

    [Fact]
    public async Task ShouldFailOnSecondValidation()
    {
        var record = new TestRecord();

        _firstValidator.Setup(x => x.ValidateOnSave(record)).ReturnsAsync(new Result(true));
        _secondValidator.Setup(x => x.ValidateOnSave(record)).ReturnsAsync(new Result(false, "Bad record"));

        var result = await _service.ValidateOnSave(record);

        Assert.False(result.Success);
        Assert.Equal("Bad record", result.ErrorMessage);
        _firstValidator.Verify(x => x.ValidateOnSave(record), Times.Once);
        _secondValidator.Verify(x => x.ValidateOnSave(record), Times.Once);
    }
}