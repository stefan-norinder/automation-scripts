using System;

namespace auto
{
    public class SearchStringNotFoundException : Exception
    {
        public SearchStringNotFoundException(string searchString) : base($"Unable to find string {searchString}")
        { }
    }
}
