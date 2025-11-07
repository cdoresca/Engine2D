using _2D_engine.Algebre;
using _2D_engine.Figure;
using GT = _2D_engine.Algebre.GeomatricTransform;

namespace _2D_engine.Acceleration
{
    internal class GridAccelarator : Accelarator
    {

        int[] nCell = new int[3];
        Vecteur cellWidth;
        Vecteur invCellWidth;
        Cell[,,] cell;

        GridAccelarator(List<Forme> obj) : base(obj)
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

        public bool Intersect(Ray ray)
        {
            if (!boundingBox(ray)) return false;

            

            int x = SpaceToCell(ray.origine, 0);
            int y = SpaceToCell(ray.origine, 1);
            int z = SpaceToCell(ray.origine, 2);
            
            Vecteur entree = new Vecteur(x, y, z);  
            Vecteur step = new Vecteur();
            Vecteur next = new Vecteur();
            Vecteur tmax = new Vecteur();
            Vecteur tdelta = new Vecteur();
          

            for (int i = 0; i < 3; i++) {
                if (ray.directeur[i] > 0) step[i] = 1;
                else if(ray.directeur[i] < 0) step[i] = -1;
                else step[i] = 0;

                if (ray.directeur[i] > 0) next[i] = box.min[i] + (entree[i] + 1) * cellWidth[i];
                else next[i] = box.min[i] + entree[i] * cellWidth[i];

                tmax[i] = (next[i] - ray.origine[i]) / ray.directeur[i];

                tdelta[i] = cellWidth[i] / Math.Abs(ray.directeur[i]);
            }

            while (true)
            {
                if (!cell[x, y, z].Intersect(ray)) return false;

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
        }

        public void CreateBox()
        {
            box = formes[0].box;

            for (int i = 0; i < formes.Count; i++)
            {
                box = box.Combine(formes[i].box);
            }

        }

        public void CalculNombreCellule()
        {
            Vecteur longeurAxe = box.max - box.min;

            int maxAxe = longeurAxe[0] > longeurAxe[1] && longeurAxe[0] > longeurAxe[2] ?
                0 : longeurAxe[1] > longeurAxe[2] ?
                1 : 2;

            nCell[maxAxe] = (int)(3 * Math.Pow(formes.Count, 1 / 3));

            for (int i = 0; i < formes.Count; i++)
            {

                if (i != maxAxe)
                {
                    nCell[i] = (int)((longeurAxe[i] * nCell[maxAxe]) / longeurAxe[maxAxe]);
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

            cell = new Cell[nCell[0], nCell[1], nCell[3]];

            for (int i = 0; i < nCell[0]; i++)
            {
                for (int j = 0; j < nCell[1]; j++)
                {
                    for(int k = 0; k < nCell[2]; k++)
                        cell[i,j,k] = new Cell();
                }
            }

            foreach (var obj in formes)
            {
                foreach (var coin in obj.box.Sommet())
                {                    
                       cell[SpaceToCell(coin, 0), SpaceToCell(coin, 1), SpaceToCell(coin, 2)].Add(obj);                  
                }
            }
        }

    }
}
