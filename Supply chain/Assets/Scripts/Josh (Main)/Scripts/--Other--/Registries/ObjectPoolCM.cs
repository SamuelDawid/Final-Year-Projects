using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolCM : MonoBehaviour
{
    Queue<GameObject> Carbon = new Queue<GameObject>();
    Queue<GameObject> Iron = new Queue<GameObject>();
    Queue<GameObject> Nickel = new Queue<GameObject>();
    Queue<GameObject> Rubber = new Queue<GameObject>();
    Queue<GameObject> Steel = new Queue<GameObject>();
    Queue<GameObject> Tungsten = new Queue<GameObject>();
    Queue<GameObject> Wood = new Queue<GameObject>();

    static public ObjectPoolCM opCMInst { get; private set; }

    void Start()
    {
        opCMInst = this;

        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(300), Carbon);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(301), Iron);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(302), Nickel);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(303), Rubber);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(304), Steel);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(305), Tungsten);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(306), Wood);
    }

    public GameObject AccquireMaterial(int ID)
    {
        Queue<GameObject> selectedMaterial = null;

        switch (ID)
        {
            case 300: selectedMaterial = Carbon; break;
            case 301: selectedMaterial = Iron; break;
            case 302: selectedMaterial = Nickel; break;
            case 303: selectedMaterial = Rubber; break;
            case 304: selectedMaterial = Steel; break;
            case 305: selectedMaterial = Tungsten; break;
            case 306: selectedMaterial = Wood; break;
            default: return null;
        }

        GameObject material;

        if (selectedMaterial.Count > 0)
        {
            material = selectedMaterial.Dequeue();
            material.SetActive(true);
        }
        else
        {
            material = Instantiate(ItemRegistration.ir.GetCraftingMaterial(ID).GivenCraftingMaterial);
            material.transform.parent = transform;
            material.SetActive(true);
        }
        return material;
    }

    public void ReturnMaterial(GameObject gm)
    {
        Queue<GameObject> selectedMaterial = null;

        switch (gm.GetComponent<CraftingMaterial>().info.GivenID)
        {
            case 300: selectedMaterial = Carbon; break;
            case 301: selectedMaterial = Iron; break;
            case 302: selectedMaterial = Nickel; break;
            case 303: selectedMaterial = Rubber; break;
            case 304: selectedMaterial = Steel; break;
            case 305: selectedMaterial = Tungsten; break;
            case 306: selectedMaterial = Wood; break;
            default: return;
        }

        selectedMaterial.Enqueue(gm);
        gm.SetActive(false);
    }

    void SpawnMaterials(CraftingMaterialData cm, Queue<GameObject> cmQueue)
    {
        for(int a = 0; a < 10; a++)
        {
            GameObject gm = Instantiate(cm.GivenCraftingMaterial);
            cmQueue.Enqueue(gm);
            gm.transform.parent = transform;
            gm.SetActive(false);
        }
    }
}

// Enemies & Healing Items

/*
public class ObjectPoolEnemies : MonoBehaviour
{
    Queue<GameObject> WheeledRobots = new Queue<GameObject>();
    Queue<GameObject> StationaryRobots = new Queue<GameObject>();

    static public ObjectPoolCM inst { get; private set; }

    void Start()
    {
        inst = this;

        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(300), Carbon);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(301), Iron);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(302), Nickel);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(303), Rubber);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(304), Steel);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(305), Tungsten);
        SpawnMaterials(ItemRegistration.ir.GetCraftingMaterial(306), Wood);
    }

    public GameObject AccquireMaterial(int ID)
    {
        Queue<GameObject> selectedMaterial = null;

        switch (ID)
        {
            case 300: selectedMaterial = Carbon; break;
            case 301: selectedMaterial = Iron; break;
            case 302: selectedMaterial = Nickel; break;
            case 303: selectedMaterial = Rubber; break;
            case 304: selectedMaterial = Steel; break;
            case 305: selectedMaterial = Tungsten; break;
            case 306: selectedMaterial = Wood; break;
            default: return null;
        }

        GameObject material;

        if (selectedMaterial.Count > 0)
        {
            material = selectedMaterial.Dequeue();
            material.SetActive(true);
        }
        else
        {
            material = Instantiate(ItemRegistration.ir.GetCraftingMaterial(ID).GivenCraftingMaterial);
            material.transform.parent = transform;
            material.SetActive(true);
        }
        return material;
    }

    public void ReturnMaterial(GameObject gm)
    {
        Queue<GameObject> selectedMaterial = null;

        switch (gm.GetComponent<CraftingMaterial>().info.GivenID)
        {
            case 300: selectedMaterial = Carbon; break;
            case 301: selectedMaterial = Iron; break;
            case 302: selectedMaterial = Nickel; break;
            case 303: selectedMaterial = Rubber; break;
            case 304: selectedMaterial = Steel; break;
            case 305: selectedMaterial = Tungsten; break;
            case 306: selectedMaterial = Wood; break;
            default: return;
        }

        selectedMaterial.Enqueue(gm);
        gm.SetActive(false);
    }

    void SpawnMaterials(CraftingMaterialData cm, Queue<GameObject> cmQueue)
    {
        for (int a = 0; a < 10; a++)
        {
            GameObject gm = Instantiate(cm.GivenCraftingMaterial);
            cmQueue.Enqueue(gm);
            gm.transform.parent = transform;
            gm.SetActive(false);
        }
    }
}*/