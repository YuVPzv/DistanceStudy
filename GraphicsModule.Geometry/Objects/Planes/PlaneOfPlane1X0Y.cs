﻿using System;
using System.Drawing;
using System.Linq;
using GraphicsModule.Geometry.Interfaces;
using GraphicsModule.Geometry.Objects.Lines;
using GraphicsModule.Geometry.Objects.Points;
using GraphicsModule.Settings;

namespace GraphicsModule.Geometry.Objects.Planes
{
    public class PlaneOfPlane1X0Y
    {
        public IObject[] Objects;
        private Name _name;
        public PlaneOfPlane1X0Y()
        {
            Objects = new IObject[3];
        }
        public PlaneOfPlane1X0Y(Point2D[] pts)
        {
            Objects = new IObject[pts.Length];
            Array.Copy(pts, Objects, pts.Length);
        }
        public PlaneOfPlane1X0Y(PointOfPlane1X0Y pt1, PointOfPlane1X0Y pt2, PointOfPlane1X0Y pt3)
        {
            Objects = new IObject[] { pt1, pt2, pt3 };
        }
        public PlaneOfPlane1X0Y(LineOfPlane1X0Y ln1, PointOfPlane1X0Y pt1)
        {
            Objects = new IObject[] { ln1, pt1 };
        }
        public PlaneOfPlane1X0Y(LineOfPlane1X0Y ln1, LineOfPlane1X0Y ln2)
        {
            Objects = new IObject[] { ln1, ln2 };
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
            _name = new Name(_name);
        }
    }
}
