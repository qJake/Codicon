namespace Codicon
{
    /// <summary>
    /// Implementers of this interface are able to read a matrix definition and interpret it into an image-based format.
    /// </summary>
    /// <typeparam name="TReturn">The output type of the interpreter.</typeparam>
    public interface IMatrixInterpreter<out TReturn>
    {
        TReturn Process(Matrix matrix);
        TReturn Process(Matrix matrix, InterpreterOptions options);
    }
}