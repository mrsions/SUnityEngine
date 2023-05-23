using UnityEngine;
using UnityEngine.Builder;
using UnityEngine.SceneManagement;

ApplicationBuilder builder = new ApplicationBuilder();

builder.ConfigureBasePath(".");

builder.ConfigureResources((resources) =>
{
    new SceneInstance();
});

builder.ConfigureSceneManager((sceneManager) =>
{
});

IApplication app = builder.Build();

app.Run();