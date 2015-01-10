using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PuzzleEd.Scripts.Regular.Actions.Interfaces
{
    public interface IDroppable
    {
        /// <summary>
        ///     Idroppable Id to match up with Idraggable
        /// </summary>
        string DropId { get; set; }

        /// <summary>
        ///     Callback called when dropping the object
        /// </summary>
        Action OnDrop { get; }

        /// <summary>
        ///     Callback called when an draggable object is hovering over a matching drop area
        /// </summary>
        Action OnOver { get; }

        /// <summary>
        ///     Callback called when dragging an object out of a drop area
        /// </summary>
        Action OnOut { get; }
    }
}
