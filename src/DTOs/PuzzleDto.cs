using System.Collections.Generic;

namespace madlib_core.DTOs
{
    public class PuzzleDto
    {
        public string Title { get; set; }
        public IEnumerable<TextComponentDto> Texts { get; set; }
    }
}