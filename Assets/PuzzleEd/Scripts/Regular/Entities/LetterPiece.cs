using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.Actions;

namespace Assets.PuzzleEd.Scripts.Regular.Entities
{
    public class LetterPiece : Piece
    {
        public string Character { get; set; }
        public bool IsEnglish = true;
    }
}
