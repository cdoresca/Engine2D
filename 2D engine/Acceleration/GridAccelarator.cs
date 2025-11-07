using _2D_engine.Algebre;
using _2D_engine.Figure;

namespace _2D_engine.Acceleration
{
    internal class GridAccelarator : Accelarator
    {

        int[] nCell = new int[3];
        Vecteur cellWidth;
        Vecteur invCellWidth;
        List<List<Cell>> cell;

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
            if (!boundingBox(ray, out double tmin)) return false;

            Ray localRay = GT.TransformRay(ray, transform.matrix);



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

            cell = new List<List<Cell>>();

            for (int i = 0; i < 3; i++)
            {

                cell[i] = new List<Cell>();

                for (int j = 0; j < nCell[i]; j++)
                {
                    cell[i].Add(new Cell());
                }
            }

            foreach (var obj in formes)
            {

                foreach (var coin in obj.box.Sommet())
                {
                    for (int i = 0; i < 3; i++)
                    {
                        cell[i][SpaceToCell(coin, i)].Add(obj);

                    }
                }
            }
        }

    }
}
