namespace Codicon.Utility
{
    internal class MatrixReader
    {
        private Matrix Matrix { get; set; }
        public char CurrentChar { get; private set; }

        public char NextSequentialCharacter
        {
            get
            {
                if (CurrentChar == '\0')
                {
                    return '1';
                }
                if (CurrentChar == '9')
                {
                    return 'a';
                }
                if (CurrentChar == 'z')
                {
                    return 'A';
                }
                if (CurrentChar == 'Z')
                {
                    return '\0';
                }
                return (char)(CurrentChar + 1);
            }
        }

        public MatrixReader(Matrix matrix)
        {
            Matrix = matrix;
            CurrentChar = '\0';
        }
        public MatrixReader(Matrix matrix, char start)
        {
            Matrix = matrix;
            CurrentChar = start;
        }

        public bool MoveNext()
        {
            do
            {
                // Increment and return false if we reach the end of the line
                if (!Increment())
                {
                    return false;
                }
            }
            while (!Matrix.HasCharacter(CurrentChar));
            return true;
        }

        public char PeekCharacter()
        {
            var internalReader = new MatrixReader(Matrix, CurrentChar);
            if (internalReader.MoveNext())
            {
                return internalReader.CurrentChar;
            }
            return '\0';
        }

        private bool Increment()
        {
            CurrentChar = NextSequentialCharacter;
            return CurrentChar != '\0';
        }
    }
}
