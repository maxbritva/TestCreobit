using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class CardPositions
    {
        public static List<Vector3> BoardPositions { get; } = new List<Vector3>()
        {
            new (7.005f,10.2f,0), new (21.015f,10.2f,0), new (35.025f,10.2f,0), //first line
            
            new (4.67f, 6.8f,0), new (9.34f, 6.8f,0), new (18.68f, 6.8f,0), 
            new (23.35f, 6.8f,0), new (32.69f, 6.8f,0), new (37.36f, 6.8f,0), // second line
            
            new (2.335f, 3.4f,0), new (7.005f, 3.4f,0), new (11.675f, 3.4f,0),
            new (16.345f, 3.4f,0), new (21.015f, 3.4f,0), new (25.685f, 3.4f,0),
            new (30.355f, 3.4f,0), new (35.025f, 3.4f,0), new (39.695f, 3.4f,0), // third line
            
            new (0,0,0), new (4.67f,0,0) , new (9.34f,0,0), new (14.01f,0,0),
            new (18.68f,0,0), new (23.35f,0,0) , new (28.02f,0,0), 
            new (32.69f,0,0), new (37.36f,0,0) , new (42.03f,0,0), // four line
        };

        public static Vector3 DeckPosition { get; } = new (14.01f, -9.5f,0);
        
        public static Vector3 CurrentCardPosition { get; } = new (28.02f, -9.5f,0);

    }
}