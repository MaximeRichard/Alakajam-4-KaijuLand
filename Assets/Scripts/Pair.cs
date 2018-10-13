using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair<T1, T2> {

    public Pair(T1 a, T2 b)
    {
        A = a;
        B = b;
    }

    public T1 A { get; set; }
    public T2 B{ get; set; }

    public override bool Equals(object obj)
    {
        return obj == (object)B;
    }

    public override int GetHashCode()
    {
        var hashCode = -1817952719;
        hashCode = hashCode * -1521134295 + EqualityComparer<T1>.Default.GetHashCode(A);
        hashCode = hashCode * -1521134295 + EqualityComparer<T2>.Default.GetHashCode(B);
        return hashCode;
    }
}
