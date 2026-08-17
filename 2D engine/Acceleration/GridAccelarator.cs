using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Trace;

namespace _2D_engine.Acceleration
{
    internal class GridAccelarator : Accelarator
    {

        int[] nCell = new int[3];
        Vecteur cellWidth;
        Vecteur invCellWidth;
        Cell[,,] cell;
        BoundingBox box;

        public GridAccelarator(List<Forme> obj) : base(obj)
        {
            BuildCell();


        }

        public double CellToSpace(int a_p, int axis)
        {
            return box.min[axis] + a_p * cellWidth[axis];
        }

        public int SpaceToCell(Point a_p, int axis)
        {
            int v = (int)((a_p[axis] - box.min[axis]) * invCellWidth[axis]);
            return Math.Max(0, Math.Min(v, nCell[axis] - 1));
        }

        public override bool Intersection(Ray ray, ref Intersection info)
        {
            
            double t;
            if (!box.Intersects(ray, out t)) return false;



            int x = SpaceToCell(ray.at(t), 0);
            int y = SpaceToCell(ray.at(t), 1);
            int z = SpaceToCell(ray.at(t), 2);

            Vecteur entree = new Vecteur(x, y, z);
            Vecteur step = new Vecteur();
            Vecteur next = new Vecteur();
            Vecteur tmax = new Vecteur();
            Vecteur tdelta = new Vecteur();


            for (int i = 0; i < 3; i++)
            {
                if (ray.directeur[i] == 0)
                {
                    step[i] = 0;
                    tmax[i] = double.PositiveInfinity;
                    tdelta[i] = double.PositiveInfinity;
                }
                else
                {
                    step[i] = ray.directeur[i] > 0 ? 1 : -1;
                    next[i] = box.min[i] + (step[i] > 0 ? (entree[i] + 1) : entree[i]) * cellWidth[i];
                    tmax[i] = (next[i] - ray.origine[i]) / ray.directeur[i];
                    tdelta[i] = cellWidth[i] / Math.Abs(ray.directeur[i]);
                }
            }

            while (x >= 0 && x < nCell[0] && y >= 0 && y < nCell[1] && z >= 0 && z < nCell[2])
            {
                if (cell[x, y, z].Intersect(ray, ref info)) 
                {
                        if (x == SpaceToCell(info.point, 0) 
                            && y == SpaceToCell(info.point, 1) 
                            && z == SpaceToCell(info.point, 2)) return true;

                } 
                
                
                    if (tmax[0] < tmax[1] && tmax[0] < tmax[2])
                    {
                        x += (int)step[0];
                        tmax[0] += tdelta[0];
                    }
                    else if (tmax[1] < tmax[2])
                    {
                        y += (int)step[1];
                        tmax[1] += tdelta[1];
                    }
                    else
                    {
                        z += (int)step[2];
                        tmax[2] += tdelta[2];
                    }
                
            }

            return false;
        }

        public void CreateBox()
        {


            box = formes[0].WorldBox;

            foreach (var forme in formes)
            {

                box = box.Combine(forme.WorldBox);
            }

        }

        public void CalculNombreCellule()
        {
            Vecteur longeurAxe = box.max - box.min;

            int maxAxe = longeurAxe[0] > longeurAxe[1] && longeurAxe[0] > longeurAxe[2] ?
                0 : longeurAxe[1] > longeurAxe[2] ?
                1 : 2;

            nCell[maxAxe] = (int)(3 * Math.Cbrt(formes.Count));

            for (int i = 0; i < 3; i++)
            {

                if (i != maxAxe)
                {
                    nCell[i] = Math.Max(1, (int)((longeurAxe[i] * nCell[maxAxe]) / longeurAxe[maxAxe]));
                }
            }
        }

        public void TailleCell()
        {
            cellWidth = box.max - box.min;

            for (int i = 0; i < 3; i++)
            {
                cellWidth[i] /= nCell[i];

            }

            invCellWidth = 1 / cellWidth;
        }

        public void BuildCell()
        {
            CreateBox();
            CalculNombreCellule();
            TailleCell();

            cell = new Cell[nCell[0], nCell[1], nCell[2]];

            for (int i = 0; i < nCell[0]; i++)
            {
                for (int j = 0; j < nCell[1]; j++)
                {
                    for (int k = 0; k < nCell[2]; k++)
                        cell[i, j, k] = new Cell();
                }
            }
            foreach (var item in formes)
            {
                Point min = item.WorldBox.min;
                Point max = item.WorldBox.max;
                int xMin = SpaceToCell(min, 0);
                int xMax = SpaceToCell(max, 0);
                int yMin = SpaceToCell(min, 1);
                int yMax = SpaceToCell(max, 1);
                int zMin = SpaceToCell(min, 2);
                int zMax = SpaceToCell(max, 2);

                for (int x = xMin; x <= xMax; x++)
                    for (int y = yMin; y <= yMax; y++)
                        for (int z = zMin; z <= zMax; z++)
                            cell[x, y, z].Add(item);
            }
        }

        public override double Surface()
        {
            throw new NotImplementedException();
        }

        public override Normal GetNormal(Point point)
        {
            throw new NotImplementedException();
        }

        public override void GetUV(Point point, out double u, out double v)
        {
            throw new NotImplementedException();
        }

        public override Point Sample()
        {
            throw new NotImplementedException();
        }

        public override double pdf()
        {
            throw new NotImplementedException();
        }

        
    }
}
