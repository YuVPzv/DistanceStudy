﻿using System;
using System.Drawing;
using System.Linq;
using GraphicsModule.Geometry.Interfaces;
using GraphicsModule.Geometry.Objects.Lines;
using GraphicsModule.Geometry.Objects.Points;
using GraphicsModule.Settings;

namespace GraphicsModule.Geometry.Objects.Planes
{
    public class Plane2D : IObject
    {
        public IObject[] Objects;
        private Name _name;
        public Plane2D()
        {
            Objects = new IObject[3];
            _name = new Name();
        }
        public Plane2D(Point2D[] pts)
        {
            Objects = new IObject[pts.Length];
            Array.Copy(pts, Objects, pts.Length);
            _name = new Name();
        }
        public Plane2D(Point2D pt1, Point2D pt2, Point2D pt3)
        {
            Objects = new IObject[] { pt1, pt2, pt3 };
            _name = new Name();
        }
        public Plane2D(Line2D ln1, Point2D pt1)
        {
            Objects = new IObject[] {ln1, pt1};
            _name = new Name();
        }
        public Plane2D(Line2D ln1, Line2D ln2)
        {
            Objects = new IObject[] { ln1, ln2 };
            _name = new Name();
        }
        public void Draw(DrawS st, Point frameCenter, Graphics graphics)
        {
            foreach (var obj in Objects)
            {
                obj.Draw(st, frameCenter, graphics);
            }
        }
        public bool IsSelected(Point mousecoords, float ptR, Point frameCenter, double distance)
        {
            return Objects.Any(obj => obj.IsSelected(mousecoords, ptR, frameCenter, distance));
        }
        public Name GetName()
        {
            return _name;
        }
        public void SetName(Name name)
        {
            _name = new Name(name);
            foreach (var t in Objects)
            {
                var n = t.GetName();
                n.Value += _name.Value;
                t.SetName(n);
            }
        }
    }
}
