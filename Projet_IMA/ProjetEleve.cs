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

            V3 camera = new V3(200, -1000, 200);

            List<Objet3D> objlist = new List<Objet3D>();

            Sphere s1 = new Sphere(new V3(300, 0, 300), 200, new Texture("carreau.jpg"), T_bump);
            Sphere s2 = new Sphere(new V3(450, 0, 200), 100, new Texture("lead.jpg"), null);
            objlist.Add(s1);
            objlist.Add(s2);

            // RAY CASTING
            //générer des ray
                //parcourir la liste des objets
                    //appeler leur fonction de test d'intersection
                        //inv coordonnées
                            // compute lights sur ce point de cet objet (->N)
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
                            }
                        }                          

                    }
                }
            }




            //s1.Draw(C_ambiant, L, camera);
            //s2.Draw(C_ambiant, L, camera);

            //Rect r1 = new Rect(new V3(150, 0, 150), new V3(300, 0, 0), new V3(0, 0, 300), new Texture("carreau.jpg"), T_bump);
            //Rect r2 = new Rect(new V3(100, 0, 100), new V3(100, 0, 0), new V3(100, 0, 100), null, null);
            //r1.Draw(C_ambiant, L, camera);
            //r2.Draw(C_ambiant, L, camera);

        }
    }
}
