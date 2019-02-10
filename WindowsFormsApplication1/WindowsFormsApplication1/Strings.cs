using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Strings
    {
        public string daneUczace()
        {
            return "5.1,3.5,1.4,0.2,Iris-setosa" + "\n4.9,3.0,1.4,0.2,Iris-setosa" +
                                "\n4.7,3.2,1.3,0.2,Iris-setosa" + "\n4.6,3.1,1.5,0.2,Iris-setosa" +
                                "\n5.0,3.6,1.4,0.2,Iris-setosa" + "\n5.4,3.9,1.7,0.4,Iris-setosa" +
                                "\n4.6,3.4,1.4,0.3,Iris-setosa" + "\n5.0,3.4,1.5,0.2,Iris-setosa" +
                                "\n4.4,2.9,1.4,0.2,Iris-setosa" + "\n4.9,3.1,1.5,0.1,Iris-setosa" +
                                "\n5.4,3.7,1.5,0.2,Iris-setosa" + "\n4.8,3.4,1.6,0.2,Iris-setosa" +
                                "\n4.8,3.0,1.4,0.1,Iris-setosa" + "\n4.3,3.0,1.1,0.1,Iris-setosa" +
                                "\n5.8,4.0,1.2,0.2,Iris-setosa" + "\n5.4,3.9,1.3,0.4,Iris-setosa" +
                                "\n5.1,3.5,1.4,0.3,Iris-setosa" + "\n5.7,3.8,1.7,0.3,Iris-setosa" +
                                "\n5.1,3.8,1.5,0.3,Iris-setosa" + "\n7.0,3.2,4.7,1.4,Iris-versicolor" +
                                "\n6.4,3.2,4.5,1.5,Iris-versicolor" + "\n6.9,3.1,4.9,1.5,Iris-versicolor" +
                                "\n5.5,2.3,4.0,1.3,Iris-versicolor" + "\n6.5,2.8,4.6,1.5,Iris-versicolor" +
                                "\n5.7,2.8,4.5,1.3,Iris-versicolor" + "\n6.3,3.3,4.7,1.6,Iris-versicolor" +
                                "\n4.9,2.4,3.3,1.0,Iris-versicolor" + "\n5.2,2.7,3.9,1.4,Iris-versicolor" +
                                "\n5.0,2.0,3.5,1.0,Iris-versicolor" + "\n6.0,2.2,4.0,1.0,Iris-versicolor" +
                                "\n6.1,2.9,4.7,1.4,Iris-versicolor" + "\n6.7,3.1,4.4,1.4,Iris-versicolor" +
                                "\n5.6,3.0,4.5,1.5,Iris-versicolor" + "\n5.8,2.7,4.1,1.0,Iris-versicolor" +
                                "\n6.2,2.2,4.5,1.5,Iris-versicolor" + "\n5.6,2.5,3.9,1.1,Iris-versicolor" +
                                "\n5.9,3.2,4.8,1.8,Iris-versicolor" + "\n6.1,2.8,4.0,1.3,Iris-versicolor" +
                                "\n6.3,3.3,6.0,2.5,Iris-virginica" + "\n5.8,2.7,5.1,1.9,Iris-virginica" +
                                "\n7.1,3.0,5.9,2.1,Iris-virginica" + "\n6.3,2.9,5.6,1.8,Iris-virginica" +
                                "\n6.5,3.0,5.8,2.2,Iris-virginica" + "\n7.6,3.0,6.6,2.1,Iris-virginica" +
                                "\n4.9,2.5,4.5,1.7,Iris-virginica" + "\n7.3,2.9,6.3,1.8,Iris-virginica" +
                                "\n6.7,2.5,5.8,1.8,Iris-virginica" + "\n7.2,3.6,6.1,2.5,Iris-virginica" +
                                "\n6.5,3.2,5.1,2.0,Iris-virginica" + "\n6.4,2.7,5.3,1.9,Iris-virginica" +
                                "\n6.8,3.0,5.5,2.1,Iris-virginica" + "\n5.7,2.5,5.0,2.0,Iris-virginica" +
                                "\n5.8,2.8,5.1,2.4,Iris-virginica" + "\n6.4,3.2,5.3,2.3,Iris-virginica" +
                                "\n6.5,3.0,5.5,1.8,Iris-virginica" + "\n7.7,3.8,6.7,2.2,Iris-virginica" +
                                "\n7.7,2.6,6.9,2.3,Iris-virginica" + "\n6.0,2.2,5.0,1.5,Iris-virginica";
        }

        public string chooseWinner(int win)
        {
            switch (win)
            {
                case 1:
                    return "Iris-setosa";
                case 2:
                    return "Iris-versicolor";
                case 3:
                    return "Iris-virginica";
                default: return "";
            }
        }
    }
}
