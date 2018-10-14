using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTemplateManager : MonoBehaviour {



    public List<Pair<bool, BreakableRock>> rocksToBreak;
    public GameObject breakPrefab;

    private void Awake()
    {
        rocksToBreak = new List<Pair<bool, BreakableRock>>();
    }

    public void RegisterRock(BreakableRock go)
    {
        rocksToBreak.Add(new Pair<bool, BreakableRock>(false, go));
    }

    public void OnEndReached()
    {

    }

    bool IsFinished
    {
        get
        {
            foreach (var item in rocksToBreak)
            {
                if (!item.A)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public void OnRockBroken(BreakableRock rock)
    {
        foreach (var item in rocksToBreak)
        {
            if (item.B == rock)
            {
                item.A = true;
            }
        }
        GameObject go = Instantiate(breakPrefab);
        go.transform.localScale = rock.transform.lossyScale;
        go.transform.position = rock.transform.position;
        go.transform.rotation = rock.transform.rotation;
        Destroy(rock.gameObject);

        if(IsFinished) GameManager.Instance.OnWin();
    }
}
