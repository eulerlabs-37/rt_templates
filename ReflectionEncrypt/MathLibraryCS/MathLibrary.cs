using System;
using System.Reflection;


namespace MathLibraryCS
{
    public class MathLibrary
    {
        int previous_;  // Previous value, if any
        int current_;   // Current sequence value
        int index_;               // Current seq. position

        public MathLibrary()
        {
            index_ = 0;
            current_ = 1;
            previous_ = 1; // see special case when initialized
        }

        private MathLibrary(int previous, int current)
        {
            index_ = 0;
            current_ = current;
            previous_ = previous; // see special case when initialized
        }

        // Produce the next value in the sequence.
        // Returns true on success, false on overflow.
        public bool fibonacci_next()
        {

            // Special case when index == 0, just return b value
            if (index_ > 0)
            {
                // otherwise, calculate next sequence value
                previous_ += current_;
            }

            int tmp = current_;
            current_ = previous_;
            previous_ = tmp;

            ++index_;
            return true;
        }

        // Get the current value in the sequence.
        public int fibonacci_current()
        {
            return current_;
        }

        // Get the current index position in the sequence.
        public int fibonacci_index()
        {
            return index_;
        }
    };
}