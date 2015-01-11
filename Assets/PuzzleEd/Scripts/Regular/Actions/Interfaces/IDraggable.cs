using System;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Actions.Interfaces
{
    public interface IDraggable
    {
        /// <summary>
        ///     Idraggable Id to match up with Idroppable
        /// </summary>
        string DragId { get; set; }

        /// <summary>
        ///     Move Object to starting position if not dropped in correct area
        /// </summary>
        bool RevertPosition { get; set; }

        /// <summary>
        ///     Starting Position of the Idraggable when first clicked
        /// </summary>
        Vector3 StartingPosition { get; set; }

        /// <summary>
        ///     Hold last object it collided with
        /// </summary>
        GameObject LastObjectCollided { get; set; }

        /// <summary>
        ///     Check if object is allowe to move.
        /// </summary>
        bool Draggable { get; set; }

        /// <summary>
        ///     Callback called when starting the drag
        /// </summary>
        Action OnStart { get; }

        /// <summary>
        ///     Callback called when stopping the drag
        /// </summary>
        Action OnStop { get; }

        /// <summary>
        ///     Callback called while dragging
        /// </summary>
        Action OnDrag { get; }
    }
}
