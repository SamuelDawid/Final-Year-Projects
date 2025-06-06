using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ipickup
{
    void grab();
    void drop();
    void addrot(float rot);
    Vector3 getpos();
    void setpos(Vector3 pos);

}
