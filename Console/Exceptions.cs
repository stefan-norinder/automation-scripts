using System;

namespace Console
{
    public class SearchStringNotFoundException : Exception
    {
        public SearchStringNotFoundException(string searchString) : base($"Unable to find string {searchString}")
        { }
    }
}
