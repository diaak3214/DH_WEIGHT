using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DK_WEIGHT.Manager.Common.util
{
    public static class FormHelper
    {
        public static void SetMovingForm(this Form form, Control[] triggerControls, Cursor movingCursorOrNull = null)
        {
            bool mouseDown = false;
            Point lastMousePoint = new Point();
            Cursor movingCursor;

            if (movingCursorOrNull == null)
                movingCursor = Cursors.NoMove2D;
            else
                movingCursor = movingCursorOrNull;

            Dictionary<Control, Cursor> oldCursors = new Dictionary<Control, Cursor>();

            foreach (var item in triggerControls)
            {
                Control con = item;

                con.MouseDown += (s, e) =>
                {
                    var thisControl = s as Control;

                    mouseDown = true;

                    if (!oldCursors.ContainsKey(item))
                        oldCursors.Add(item, thisControl.Cursor);
                    else
                        oldCursors[item] = thisControl.Cursor;

                    thisControl.Cursor = movingCursor;

                    lastMousePoint = Control.MousePosition;
                };

                con.MouseMove += (s, e) =>
                {
                    if (!mouseDown)
                        return;

                    Point currentPoint = Control.MousePosition;

                    int x = currentPoint.X - lastMousePoint.X;
                    int y = currentPoint.Y - lastMousePoint.Y;

                    form.Location = new Point(form.Location.X + x, form.Location.Y + y);

                    lastMousePoint = currentPoint;
                };

                con.MouseUp += (s, e) =>
                {
                    var thisControl = s as Control;

                    mouseDown = false;
                    thisControl.Cursor = oldCursors[thisControl];
                };
            }
        }
    }
}
