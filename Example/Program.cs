using UnityEngine;

void print(object o) => Debug.Log(o);

var pos = new Vector3(0.3124f, 0.1279f, 5.123f);
var rot = Quaternion.Euler(12.88f, 57.79f, 320f);
var scal = Vector3.one;
print(pos);
print(rot);
print(rot.eulerAngles);
print(scal);

Matrix4x4 mat = Matrix4x4.TRS(pos, rot, scal);

print(mat);
print(mat.inverse);
print(mat.transpose);
print(mat.lossyScale);
//print(mat.decomposeProjection);
print(mat.rotation);
print(mat.rotation.eulerAngles);
print((double)mat.determinant);
print(mat.MultiplyPoint(new Vector3(0.157f, 0.2257f, 52.767f)));
print(mat.MultiplyVector(new Vector3(0.157f, 0.2257f, 52.767f)));
print(mat.MultiplyPoint3x4(new Vector3(0.157f, 0.2257f, 52.767f)));
print(mat.GetColumn(0));
print(mat.GetColumn(1));
print(mat.GetColumn(2));
print(mat.GetColumn(3));
print(mat.GetPosition());
print(Matrix4x4.identity);
print(Matrix4x4.zero);
print(Matrix4x4.Translate(new Vector3(12.88f, 57.79f, 320f)));
print(Matrix4x4.Rotate(Quaternion.Euler(12.88f, 57.79f, 320f)));
print(Matrix4x4.Scale(new Vector3(12.88f, 57.79f, 320f)));