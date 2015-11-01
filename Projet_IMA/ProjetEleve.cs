using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            int width = BitmapEcran.GetWidth();
            int height = BitmapEcran.GetHeight()+1;

            ZBuffer.init();

            Lumiere L = new Lumiere(0, new V3(1.0f, -1.0f, 1.0f), new V3(0.0f, 0.0f, 0.0f), new Couleur(0.8f, 0.8f, 0.8f));

            Couleur C_ambiant = new Couleur(0.2f, 0.2f, 0.2f);
            Couleur C_lampe = new Couleur(0.8f, 0.8f, 0.8f);

            Texture T_bump = new Texture("bump38.jpg");
            Texture T_leadbump = new Texture("lead_bump.jpg");

            V3 camera = new V3(width/2, -1000, height/2);

            List<Objet3D> objlist = new List<Objet3D>();

            Sphere s1 = new Sphere(new V3(width/2, 500, height-50), 150, new Texture("carreau.jpg"), T_bump);
            Sphere s2 = new Sphere(new V3(50, 500, height/2), 150, new Texture("lead.jpg"), T_leadbump);
            //fond
            Rect r1 = new Rect(new V3(50, 1000, 50), new V3(width - 100, 0, 0), new V3(0, 0, height - 100), new Texture("aymeric.jpg"), null);
            //bas
            Rect r2 = new Rect(new V3(50, 0, 50), new V3(width - 100, 0, 0), new V3(0, 1000, 0), new Texture("stone2.jpg"), null);
            //haut
            Rect r3 = new Rect(new V3(width-50, 0, height-50), new V3(-(width-100), 0, 0), new V3(0, 1000, 0), new Texture("fibre.jpg"), null);
            //gauche
            Rect r4 = new Rect(new V3(50, 0, 50), new V3(0, 1000, 0), new V3(0, 0, height - 100), new Texture("brick01.jpg"), null);
            //droite
            Rect r5 = new Rect(new V3(width-50, 1000, 50), new V3(0, -1000, 0), new V3(0, 0, height - 100), new Texture("brick01.jpg"), null);
            //milieu
            Rect r6 = new Rect(new V3(width/2 + 100, 750, 50), new V3(0, -500, 0), new V3(0, 0, height/3), new Texture("brick01.jpg"), null);
            objlist.Add(s1);
            objlist.Add(s2);
            objlist.Add(r1);
            objlist.Add(r2);
            objlist.Add(r3);
            objlist.Add(r4);
            objlist.Add(r5);
            objlist.Add(r6);

            // RAY CASTING

            V3 Rd, R;
            //Point I;
            float t, tnew;
            //parcourir les pixels en x
            for (int pxx = 0; pxx < width; pxx++) {
                //parcourir les pixels en z
                for (int pxz = 0; pxz < height; pxz++) {
                    t = float.MaxValue;
                    // pour chaque pixel générer un rayon Rd
                    Rd = new V3(pxx - camera.x, -camera.y, pxz - camera.z);
                    Rd.Normalize();
                    // parcourir la liste des objets
                    foreach (Objet3D obj in objlist) {
                        // appeler l'intersection et récupérer le point
                        tnew = obj.getIntersect(Rd, camera);
                        if (tnew != -1)
                        {
                            if (tnew < t)
                            {
                                t = tnew;
                                // R est le point d'intersection de Rd et de l'objet
                                R = camera + t * Rd;
                                Couleur finalColor = obj.DrawPoint(C_ambiant, L, camera, R);
                                BitmapEcran.DrawPixel(pxx, pxz, finalColor);
                                //Console.WriteLine("Drawing at " + pxx + " " + pxz);
                            }
                        }                          

                    }
                }
            }




            //s1.Draw(C_ambiant, L, camera);
            //s2.Draw(C_ambiant, L, camera);

            
            //r1.Draw(C_ambiant, L, camera);
            //r2.Draw(C_ambiant, L, camera);

        }
    }
}
