using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiar_Escenas : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("Nivel");
    }
    public void exit()
    {
        Application.Quit();
    }
    public void menu()
    {
        SceneManager.LoadScene("Inicio");
    }
}
