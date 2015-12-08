using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using CsGL.OpenGL;

namespace Gwearable
{
    public class MilkshapeHeader
    {
        public char[] id;						
        public int version;
    }
    public class MilkshapeJointAndWeight
    {
        public int[] jointIndices;
        public int[] jointWeights;
        public MilkshapeJointAndWeight()
        {
            jointIndices = new int[4];
            jointWeights = new int[4];
        }
    }
    public class MilkshapeVertex
    {
        public short flags;						
        public float[] vertex;					
        public char boneId;						
        public byte referenceCount;
        public int[] boneIDs;					
        public int[] boneWeights;				
    }

    public class MilkshapeTriangle
    {
        public short flags;						
        public short[] vertexIndices;			
        public float[][] vertexNormals;			
        public float[] s;						
        public float[] t;						
        public byte smoothingGroup;				
        public byte groupIndex;					
    }
    public class MilkshapeGroup
    {
        public byte flags;						
        public char[] name;						
        public short numTriangles;				
        public short[] triangleIndices;
        public short materialIndex;

        public CustomVertexStruct[] vertices;      
        public CustomVertexStruct[] OriginalVertices;
        public int numVertices;					
        //public int startVertexIndex;			//indexul din bufferul mare de la care incepe desenarea
        //public CustomVertex.PositionNormalTextured[] userVertices;   //aici stocam punctele pentru userprimitive
    }
    public class MilkshapeMaterial
    {
        public char[] name;						
        public float[] ambient;					
        public float[] diffuse;					
        public float[] specular;				
        public float[] emissive;				
        public float shininess;					
        public float transparency;				
        public char mode;						
        public byte[] textureFN;				
        public byte[] spheremapFN;				
        public string textureS;					
        public string sphereMapS;				
        //implementarea DirectX
        //public Grap material;				
        //public Material material;
        //public Texture texture;			
        //public Texture spheremap;			
    }
    public class MilkshapeRotationKey
    {
        public float time;						
        public float[] rotation;				
    }

    public class MilkshapePositionKey
    {
        public float time;						
        public float[] position;				
    }
    public class MilkshapeTangent
    {
        public Vector3 tangentIn;
        public Vector3 tangentOut;
    }

    public class Vector3
    {
        public float X, Y, Z;
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }
    }

    public class Vector2
    {
        public float X, Y;
        public Vector2()
        {
            X = 0.0f;
            Y = 0.0f;
        }
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public class Vector4
    {
        public float X, Y, Z, W;
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
            W = 0.0f;
        }
    }
    public class MilkshapeMatrix3x4
    {
        public float[][] v;
        public MilkshapeMatrix3x4()
        {
            v = new float[3][];
            for (int i = 0; i < 3; i++)
            {
                v[i] = new float[4];
            }
        }
    }
    
    public class MilkshapeMatrix4x4
    {
        public float[][] v;
        public MilkshapeMatrix4x4()
        {
            v = new float[4][];
            for (int i = 0; i < 4; i++)
            {
                v[i] = new float[4];
            }
        }
    }
    
    public class MilkshapeJoint
    {
        public byte flags;						
        public char[] name;						
        public char[] parentName;				
        public float[] rotation;				
        public float[] position;				
        public short numRotKeyFrames;			
        public short numPosKeyFrames;			
        public MilkshapeTangent[] tangents;	    
        public MilkshapeRotationKey[] rotKeyFrames;
        public MilkshapePositionKey[] posKeyFrames;

        public string parentNameS;
        public string nameS;
        public int parentIndex;
        public MilkshapeMatrix3x4 matLocalSkeleton;
        public MilkshapeMatrix3x4 matGlobalSkeleton;

        public MilkshapeMatrix3x4 matLocal;
        public MilkshapeMatrix3x4 matGlobal;
    }

    public class CustomVertexStruct
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 texCoord1;
        public Vector2 texCoord2;
        public Vector4 boneIDs;
        public Vector4 boneWeights;
        public int boneID;

        public CustomVertexStruct()
        {
            Position = new Vector3();
            Normal = new Vector3();
            texCoord1 = new Vector2();
            texCoord2 = new Vector2();
            boneIDs = new Vector4();
            boneWeights = new Vector4();
            boneID = 0;

        }

        //        public CustomVertexStruct
        //public static readonly VertexFormat FVF = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture0 | VertexFormats.Texture2;
        //public static int SizeInBytes { get { return sizeof(float) * 19; } }
        //         public static VertexElement[] VertexElements ={
        //                                                          new VertexElement(0,VertexElementFormat.Vector3,VertexElementUsage.Position,0),
        //                                                          new VertexElement(sizeof(float)*3,VertexElementFormat.Vector3,VertexElementUsage.Normal,0),
        //                                                          new VertexElement(sizeof(float)*6,VertexElementFormat.Vector2,VertexElementUsage.TextureCoordinate,0),
        //                                                          new VertexElement(sizeof(float)*8,VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate,1),
        //                                                          new VertexElement(sizeof(float)*10,VertexElementFormat.Vector4, VertexElementUsage.BlendIndices,0),
        //                                                          new VertexElement(sizeof(float)*14,VertexElementFormat.Vector4, VertexElementUsage.BlendWeight,0),
        //                                                          new VertexElement(sizeof(float)*18,VertexElementFormat.Short4, VertexElementUsage.BlendIndices,1)
        //};

    }
    
    public class ShaderBatch
    {
        public List<int> BoneIds = new List<int>();
        public List<int> VertexIds = new List<int>();
    }
    public class ShaderRenderer
    {
        public List<ShaderBatch> Batches = new List<ShaderBatch>();
        public void BuildBatches(MilkshapeModel model)
        {
            //primul triunghi creaza un batch
            ShaderBatch s = new ShaderBatch();
            MilkshapeTriangle t = model.Triangles[0];
            Batches.Add(s);
            AddToBatch(model, s, t);
            //triangle vertices must be rendered togheter
            for (int i = 0; i < model.numTriangles; i++)
            {
                //cautam un batch care sa contina toate bone-urile
                ShaderBatch sb = FindBatch(model.Triangles[i]);
                if (sb != null)
                {
                    AddToBatch(model, sb, model.Triangles[i]);
                }
                else
                {
                    sb = FindBestMatch(model.Triangles[i]);
                    if (sb != null)
                    {
                        AddToBatch(model, sb, model.Triangles[i]);
                    }
                    else
                    {
                        ShaderBatch sbnew = new ShaderBatch();
                        AddToBatch(model, sb, model.Triangles[i]);
                        Batches.Add(sbnew);
                    }
                }

            }
        }

        private ShaderBatch FindBestMatch(MilkshapeTriangle milkshapeTriangle)
        {
            throw new NotImplementedException();
        }

        private static void AddToBatch(MilkshapeModel model, ShaderBatch s, MilkshapeTriangle t)
        {
            if (model.Vertices[t.vertexIndices[0]].boneIDs[0] != -1)
            {
                s.BoneIds.Add(model.Vertices[model.Triangles[0].vertexIndices[0]].boneIDs[0]);
            }
            if (model.Vertices[t.vertexIndices[0]].boneIDs[1] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[0]].boneIDs[1]);
            }
            if (model.Vertices[t.vertexIndices[0]].boneIDs[2] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[0]].boneIDs[2]);
            }
            if (model.Vertices[t.vertexIndices[0]].boneIDs[3] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[0]].boneIDs[3]);
            }
            if (model.Vertices[t.vertexIndices[1]].boneIDs[0] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[1]].boneIDs[0]);
            }
            if (model.Vertices[t.vertexIndices[1]].boneIDs[1] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[1]].boneIDs[1]);
            }
            if (model.Vertices[t.vertexIndices[1]].boneIDs[2] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[1]].boneIDs[2]);
            }
            if (model.Vertices[t.vertexIndices[1]].boneIDs[3] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[1]].boneIDs[3]);
            }
            if (model.Vertices[t.vertexIndices[2]].boneIDs[0] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[2]].boneIDs[0]);
            }
            if (model.Vertices[t.vertexIndices[2]].boneIDs[1] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[2]].boneIDs[1]);
            }
            if (model.Vertices[t.vertexIndices[2]].boneIDs[2] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[2]].boneIDs[2]);
            }
            if (model.Vertices[t.vertexIndices[2]].boneIDs[3] != -1)
            {
                s.BoneIds.Add(model.Vertices[t.vertexIndices[2]].boneIDs[3]);
            }
        }

        private ShaderBatch FindBatch(MilkshapeTriangle milkshapeTriangle)
        {
            for (int i = 0; i < Batches.Count; i++)
            {
                //if (
            }
            return null;
        }
    }
    public class MilkshapeModel
    {
        private MilkshapeHeader Header;
        public short numVertices;
        public MilkshapeVertex[] Vertices;
        public short numTriangles;
        public MilkshapeTriangle[] Triangles;
        public short numGroups;

        public MilkshapeGroup[] Groups;
        public short numMaterials;
        public MilkshapeMaterial[] Materials;
        private float animFPS;
        private float currentTime;
        private int totalFrames;
        private short numJoints;
        private MilkshapeJoint[] Joints;

        private void loadMS3DFromFile(string FileName)
        {
            FileStream fs = File.Open(FileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            Header = new MilkshapeHeader();
            Header.id = br.ReadChars(10);
            Header.version = br.ReadInt32();

            numVertices = br.ReadInt16();
            Vertices = new MilkshapeVertex[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                Vertices[i] = new MilkshapeVertex();
                Vertices[i].flags = br.ReadByte();
                Vertices[i].vertex = new float[3];
                Vertices[i].vertex[0] = br.ReadSingle();
                Vertices[i].vertex[1] = br.ReadSingle();
                Vertices[i].vertex[2] = br.ReadSingle();
                Vertices[i].boneId = (char)br.ReadByte();
                Vertices[i].referenceCount = br.ReadByte();
            }
            numTriangles = br.ReadInt16();
            Triangles = new MilkshapeTriangle[numTriangles];
            for (int i = 0; i < numTriangles; i++)
            {
                Triangles[i] = new MilkshapeTriangle();
                Triangles[i].flags = br.ReadInt16();
                Triangles[i].vertexIndices = new short[3];
                Triangles[i].vertexIndices[0] = br.ReadInt16();
                Triangles[i].vertexIndices[1] = br.ReadInt16();
                Triangles[i].vertexIndices[2] = br.ReadInt16();

                Triangles[i].vertexNormals = new float[3][];
                Triangles[i].vertexNormals[0] = new float[3];
                Triangles[i].vertexNormals[1] = new float[3];
                Triangles[i].vertexNormals[2] = new float[3];
                Triangles[i].vertexNormals[0][0] = br.ReadSingle();
                Triangles[i].vertexNormals[0][1] = br.ReadSingle();
                Triangles[i].vertexNormals[0][2] = br.ReadSingle();
                Triangles[i].vertexNormals[1][0] = br.ReadSingle();
                Triangles[i].vertexNormals[1][1] = br.ReadSingle();
                Triangles[i].vertexNormals[1][2] = br.ReadSingle();
                Triangles[i].vertexNormals[2][0] = br.ReadSingle();
                Triangles[i].vertexNormals[2][1] = br.ReadSingle();
                Triangles[i].vertexNormals[2][2] = br.ReadSingle();

                Triangles[i].s = new float[3];
                Triangles[i].t = new float[3];

                Triangles[i].s[0] = br.ReadSingle();
                Triangles[i].s[1] = br.ReadSingle();
                Triangles[i].s[2] = br.ReadSingle();
                Triangles[i].t[0] = br.ReadSingle();
                Triangles[i].t[1] = br.ReadSingle();
                Triangles[i].t[2] = br.ReadSingle();
                Triangles[i].smoothingGroup = br.ReadByte();
                Triangles[i].groupIndex = br.ReadByte();
            }
            //
            numGroups = br.ReadInt16();
            Groups = new MilkshapeGroup[numGroups];
            for (int i = 0; i < numGroups; i++)
            {
                Groups[i] = new MilkshapeGroup();
                Groups[i].flags = br.ReadByte();
                Groups[i].name = br.ReadChars(32);
                Groups[i].numTriangles = br.ReadInt16();
                Groups[i].triangleIndices = new short[Groups[i].numTriangles];
                for (int j = 0; j < Groups[i].numTriangles; j++)
                    Groups[i].triangleIndices[j] = br.ReadInt16();
                Groups[i].materialIndex = br.ReadSByte();
            }
            numMaterials = br.ReadInt16();
            Materials = new MilkshapeMaterial[numMaterials];
            for (int i = 0; i < numMaterials; i++)
            {
                Materials[i] = new MilkshapeMaterial();
                Materials[i].name = br.ReadChars(32);
                Materials[i].ambient = new float[4];
                Materials[i].diffuse = new float[4];
                Materials[i].emissive = new float[4];
                Materials[i].specular = new float[4];

                Materials[i].ambient[0] = br.ReadSingle();
                Materials[i].ambient[1] = br.ReadSingle();
                Materials[i].ambient[2] = br.ReadSingle();
                Materials[i].ambient[3] = br.ReadSingle();

                Materials[i].diffuse[0] = br.ReadSingle();
                Materials[i].diffuse[1] = br.ReadSingle();
                Materials[i].diffuse[2] = br.ReadSingle();
                Materials[i].diffuse[3] = br.ReadSingle();

                Materials[i].specular[0] = br.ReadSingle();
                Materials[i].specular[1] = br.ReadSingle();
                Materials[i].specular[2] = br.ReadSingle();
                Materials[i].specular[3] = br.ReadSingle();

                Materials[i].emissive[0] = br.ReadSingle();
                Materials[i].emissive[1] = br.ReadSingle();
                Materials[i].emissive[2] = br.ReadSingle();
                Materials[i].emissive[3] = br.ReadSingle();

                Materials[i].shininess = br.ReadSingle();
                Materials[i].transparency = br.ReadSingle();
                Materials[i].mode = br.ReadChar();

                string s = "";
                Materials[i].textureFN = br.ReadBytes(128);
                Materials[i].spheremapFN = br.ReadBytes(128);
                for (int j = 0; j < 128; j++)
                {
                    if (Materials[i].textureFN[j] == 0)
                        break;
                    s = s + (char)Materials[i].textureFN[j];
                }
                Materials[i].textureS = s;
                if (s.Trim() != "")
                {
                    try
                    {
                        s = s.Substring(s.LastIndexOf("/") + 1);
                        //加载纹理
                        //Materials[i].texture = Texture2D.FromStream(gd, new FileStream("model\\" + s, FileMode.Open));//TextureLoader.FromFile(dev, s); 
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
                s = "";
                for (int j = 0; j < 128; j++)
                {
                    if (Materials[i].spheremapFN[j] == 0)
                        break;
                    s = s + (char)Materials[i].spheremapFN[j];
                }
                Materials[i].sphereMapS = s;
                s = s.Substring(s.LastIndexOf("/") + 1);
                if (s.Trim() != "")
                {
                    try
                    {
                        //Materials[i].spheremap = Texture2D.FromStream(gd, new FileStream("model\\" + s, FileMode.Open));//TextureLoader.FromFile(dev, s);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            }

            //
            animFPS = br.ReadSingle();
            currentTime = br.ReadSingle();
            totalFrames = br.ReadInt32();
            //animBoundMax = totalFrames;
            numJoints = br.ReadInt16();
            Joints = new MilkshapeJoint[numJoints];
            for (int i = 0; i < numJoints; i++)
            {
                Joints[i] = new MilkshapeJoint();
                Joints[i].flags = br.ReadByte();
                Joints[i].name = br.ReadChars(32);
                Joints[i].parentName = br.ReadChars(32);
                Joints[i].nameS = "";
                for (int k = 0; k < 32; k++)
                {
                    if (Joints[i].name[k] == (char)0)
                        break;
                    Joints[i].nameS += Joints[i].name[k];
                }
                Joints[i].parentNameS = "";
                for (int k = 0; k < 32; k++)
                {
                    if (Joints[i].parentName[k] == (char)0)
                        break;
                    Joints[i].parentNameS += Joints[i].parentName[k];
                }
                Joints[i].rotation = new float[3];
                Joints[i].position = new float[3];
                Joints[i].rotation[0] = br.ReadSingle();
                Joints[i].rotation[1] = br.ReadSingle();
                Joints[i].rotation[2] = br.ReadSingle();
                Joints[i].position[0] = br.ReadSingle();
                Joints[i].position[1] = br.ReadSingle();
                Joints[i].position[2] = br.ReadSingle();
                Joints[i].numRotKeyFrames = br.ReadInt16();
                Joints[i].numPosKeyFrames = br.ReadInt16();
                Joints[i].rotKeyFrames = new MilkshapeRotationKey[Joints[i].numRotKeyFrames];
                Joints[i].posKeyFrames = new MilkshapePositionKey[Joints[i].numPosKeyFrames];
                for (int k = 0; k < Joints[i].numRotKeyFrames; k++)
                {
                    Joints[i].rotKeyFrames[k] = new MilkshapeRotationKey();
                    Joints[i].rotKeyFrames[k].time = br.ReadSingle() * animFPS;
                    Joints[i].rotKeyFrames[k].rotation = new float[3];
                    Joints[i].rotKeyFrames[k].rotation[0] = br.ReadSingle();
                    Joints[i].rotKeyFrames[k].rotation[1] = br.ReadSingle();
                    Joints[i].rotKeyFrames[k].rotation[2] = br.ReadSingle();
                }
                for (int k = 0; k < Joints[i].numPosKeyFrames; k++)
                {
                    Joints[i].posKeyFrames[k] = new MilkshapePositionKey();
                    Joints[i].posKeyFrames[k].time = br.ReadSingle() * animFPS;
                    Joints[i].posKeyFrames[k].position = new float[3];
                    Joints[i].posKeyFrames[k].position[0] = br.ReadSingle();
                    Joints[i].posKeyFrames[k].position[1] = br.ReadSingle();
                    Joints[i].posKeyFrames[k].position[2] = br.ReadSingle();
                }
            }

            if (fs.Position < fs.Length)
            {
                int subversion = br.ReadInt32();
                if (subversion == 1)
                {
                    int numComments = br.ReadInt32();
                    for (int i = 0; i < numComments; i++)
                    {
                        int index;
                        index = br.ReadInt32();
                        index = br.ReadInt32();
                        if (index > 0)
                            br.ReadChars(index);
                    }
                    numComments = br.ReadInt32();
                    for (int i = 0; i < numComments; i++)
                    {
                        int index;
                        index = br.ReadInt32();
                        index = br.ReadInt32();
                        if (index > 0)
                            br.ReadChars(index);
                    }
                    numComments = br.ReadInt32();
                    for (int i = 0; i < numComments; i++)
                    {
                        int index;
                        index = br.ReadInt32();
                        index = br.ReadInt32();
                        if (index > 0)
                            br.ReadChars(index);
                    }
                    numComments = br.ReadInt32();
                    if (numComments == 1)
                    {
                        int index = br.ReadInt32();
                        br.ReadChars(index);
                    }
                }
            }
            if (fs.Position < fs.Length)
            {
                int subversion = br.ReadInt32();
                for (int i = 0; i < numVertices; i++)
                {
                    Vertices[i].boneIDs = new int[3];
                    Vertices[i].boneWeights = new int[3];
                    if ((subversion == 1) || (subversion == 2))
                    {
                        Vertices[i].boneIDs[0] = br.ReadSByte();
                        Vertices[i].boneIDs[1] = br.ReadSByte();
                        Vertices[i].boneIDs[2] = br.ReadSByte();
                        Vertices[i].boneWeights[0] = br.ReadSByte();
                        Vertices[i].boneWeights[1] = br.ReadSByte();
                        Vertices[i].boneWeights[2] = br.ReadSByte();
                        if (subversion == 2)
                            br.ReadInt32();
                    }
                }
            }
            rebuildVertices();
            SetupJoints();
            br.Close();
            fs.Close();
        }

        public void rebuildVertices()
        {
            //int cnt = 0;
            for (int i = 0; i < numGroups; i++)
            {
                Groups[i].numVertices = Groups[i].numTriangles * 3;
                Groups[i].vertices = new CustomVertexStruct[Groups[i].numVertices];
                for (int j = 0; j < Groups[i].numVertices; j++)
                {
                    Groups[i].vertices[j] = new CustomVertexStruct();
                }

                Groups[i].OriginalVertices = new CustomVertexStruct[Groups[i].numVertices];

                for (int j = 0; j < Groups[i].numVertices; j++)
                {
                    Groups[i].OriginalVertices[j] = new CustomVertexStruct();
                }

                MilkshapeGroup group = Groups[i];
                for (int k = 0; k < Groups[i].numTriangles; k++)
                {
                    //cnt += 3;
                    Groups[i].vertices[k * 3 + 0].Position.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].vertex[0];
                    Groups[i].vertices[k * 3 + 0].Position.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].vertex[1];
                    Groups[i].vertices[k * 3 + 0].Position.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].vertex[2];
                    Groups[i].vertices[k * 3 + 0].Normal.X = Triangles[Groups[i].triangleIndices[k]].vertexNormals[0][0];
                    Groups[i].vertices[k * 3 + 0].Normal.Y = Triangles[Groups[i].triangleIndices[k]].vertexNormals[0][1];
                    Groups[i].vertices[k * 3 + 0].Normal.Z = Triangles[Groups[i].triangleIndices[k]].vertexNormals[0][2];
                    Groups[i].vertices[k * 3 + 0].texCoord1.X = Triangles[Groups[i].triangleIndices[k]].s[0];
                    Groups[i].vertices[k * 3 + 0].texCoord1.Y = Triangles[Groups[i].triangleIndices[k]].t[0];
                    Groups[i].vertices[k * 3 + 0].boneID = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneId;
                    //ms3d_group[i].vertices[k * 3 + 0].boneWeights.X = 1;
                    MilkshapeJointAndWeight tmp = new MilkshapeJointAndWeight();
                    if (Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneIDs != null)
                    {
                        Groups[i].vertices[k * 3 + 0].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneIDs[0];
                        Groups[i].vertices[k * 3 + 0].boneIDs.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneIDs[1];
                        Groups[i].vertices[k * 3 + 0].boneIDs.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneIDs[2];
                        Groups[i].vertices[k * 3 + 0].boneWeights.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneWeights[0];
                        Groups[i].vertices[k * 3 + 0].boneWeights.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneWeights[1];
                        Groups[i].vertices[k * 3 + 0].boneWeights.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneWeights[2];
                        FillJointIndicesAndWeights(Groups[i].vertices[k * 3 + 0], tmp);
                        Groups[i].vertices[k * 3 + 0].boneIDs.X = tmp.jointIndices[0];
                        Groups[i].vertices[k * 3 + 0].boneIDs.Y = tmp.jointIndices[1];
                        Groups[i].vertices[k * 3 + 0].boneIDs.Z = tmp.jointIndices[2];
                        Groups[i].vertices[k * 3 + 0].boneIDs.W = tmp.jointIndices[3];
                    }
                    else
                    {
                        Groups[i].vertices[k * 3 + 0].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneId;
                        Groups[i].vertices[k * 3 + 0].boneIDs.Y = 0;
                        Groups[i].vertices[k * 3 + 0].boneIDs.Z = 0;
                        Groups[i].vertices[k * 3 + 0].boneIDs.W = 0;
                        Groups[i].vertices[k * 3 + 0].boneWeights.X = 1;
                        Groups[i].vertices[k * 3 + 0].boneWeights.Y = 0;
                        Groups[i].vertices[k * 3 + 0].boneWeights.Z = 0;
                        Groups[i].vertices[k * 3 + 0].boneWeights.W = 0;
                    }


                    Groups[i].vertices[k * 3 + 1].Position.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].vertex[0];
                    Groups[i].vertices[k * 3 + 1].Position.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].vertex[1];
                    Groups[i].vertices[k * 3 + 1].Position.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].vertex[2];
                    Groups[i].vertices[k * 3 + 1].Normal.X = Triangles[Groups[i].triangleIndices[k]].vertexNormals[1][0];
                    Groups[i].vertices[k * 3 + 1].Normal.Y = Triangles[Groups[i].triangleIndices[k]].vertexNormals[1][1];
                    Groups[i].vertices[k * 3 + 1].Normal.Z = Triangles[Groups[i].triangleIndices[k]].vertexNormals[1][2];
                    Groups[i].vertices[k * 3 + 1].texCoord1.X = Triangles[Groups[i].triangleIndices[k]].s[1];
                    Groups[i].vertices[k * 3 + 1].texCoord1.Y = Triangles[Groups[i].triangleIndices[k]].t[1];
                    Groups[i].vertices[k * 3 + 1].boneID = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneId;
                    Groups[i].vertices[k * 3 + 1].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneId;
                    if (Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneIDs != null)
                    {
                        Groups[i].vertices[k * 3 + 1].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneIDs[0];
                        Groups[i].vertices[k * 3 + 1].boneIDs.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneIDs[1];
                        Groups[i].vertices[k * 3 + 1].boneIDs.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneIDs[2];
                        Groups[i].vertices[k * 3 + 1].boneWeights.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneWeights[0];
                        Groups[i].vertices[k * 3 + 1].boneWeights.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneWeights[1];
                        Groups[i].vertices[k * 3 + 1].boneWeights.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneWeights[2];
                        FillJointIndicesAndWeights(Groups[i].vertices[k * 3 + 0], tmp);
                        Groups[i].vertices[k * 3 + 1].boneIDs.X = tmp.jointIndices[0];
                        Groups[i].vertices[k * 3 + 1].boneIDs.Y = tmp.jointIndices[1];
                        Groups[i].vertices[k * 3 + 1].boneIDs.Z = tmp.jointIndices[2];
                        Groups[i].vertices[k * 3 + 1].boneIDs.W = tmp.jointIndices[3];
                    }
                    else
                    {
                        Groups[i].vertices[k * 3 + 1].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[1]].boneId;
                        Groups[i].vertices[k * 3 + 1].boneIDs.Y = 0;
                        Groups[i].vertices[k * 3 + 1].boneIDs.Z = 0;
                        Groups[i].vertices[k * 3 + 1].boneIDs.W = 0;
                        Groups[i].vertices[k * 3 + 1].boneWeights.X = 1;
                        Groups[i].vertices[k * 3 + 1].boneWeights.Y = 0;
                        Groups[i].vertices[k * 3 + 1].boneWeights.Z = 0;
                        Groups[i].vertices[k * 3 + 1].boneWeights.W = 0;
                    }

                    Groups[i].vertices[k * 3 + 2].Position.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].vertex[0];
                    Groups[i].vertices[k * 3 + 2].Position.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].vertex[1];
                    Groups[i].vertices[k * 3 + 2].Position.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].vertex[2];
                    Groups[i].vertices[k * 3 + 2].Normal.X = Triangles[Groups[i].triangleIndices[k]].vertexNormals[2][0];
                    Groups[i].vertices[k * 3 + 2].Normal.Y = Triangles[Groups[i].triangleIndices[k]].vertexNormals[2][1];
                    Groups[i].vertices[k * 3 + 2].Normal.Z = Triangles[Groups[i].triangleIndices[k]].vertexNormals[2][2];
                    Groups[i].vertices[k * 3 + 2].texCoord1.X = Triangles[Groups[i].triangleIndices[k]].s[2];
                    Groups[i].vertices[k * 3 + 2].texCoord1.Y = Triangles[Groups[i].triangleIndices[k]].t[2];
                    Groups[i].vertices[k * 3 + 2].boneID = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneId;
                    Groups[i].vertices[k * 3 + 2].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[0]].boneId;
                    Groups[i].vertices[k * 3 + 2].boneWeights.X = 1;
                    if (Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneIDs != null)
                    {
                        Groups[i].vertices[k * 3 + 1].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneIDs[0];
                        Groups[i].vertices[k * 3 + 2].boneIDs.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneIDs[1];
                        Groups[i].vertices[k * 3 + 2].boneIDs.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneIDs[2];
                        Groups[i].vertices[k * 3 + 2].boneWeights.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneWeights[0];
                        Groups[i].vertices[k * 3 + 2].boneWeights.Y = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneWeights[1];
                        Groups[i].vertices[k * 3 + 2].boneWeights.Z = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneWeights[2];
                        FillJointIndicesAndWeights(Groups[i].vertices[k * 3 + 0], tmp);
                        Groups[i].vertices[k * 3 + 2].boneIDs.X = tmp.jointIndices[0];
                        Groups[i].vertices[k * 3 + 2].boneIDs.Y = tmp.jointIndices[1];
                        Groups[i].vertices[k * 3 + 2].boneIDs.Z = tmp.jointIndices[2];
                        Groups[i].vertices[k * 3 + 2].boneIDs.W = tmp.jointIndices[3];
                    }
                    else
                    {
                        Groups[i].vertices[k * 3 + 2].boneIDs.X = Vertices[Triangles[Groups[i].triangleIndices[k]].vertexIndices[2]].boneId;
                        Groups[i].vertices[k * 3 + 2].boneIDs.Y = 0;
                        Groups[i].vertices[k * 3 + 2].boneIDs.Z = 0;
                        Groups[i].vertices[k * 3 + 2].boneIDs.W = 0;
                        Groups[i].vertices[k * 3 + 2].boneWeights.X = 1;
                        Groups[i].vertices[k * 3 + 2].boneWeights.Y = 0;
                        Groups[i].vertices[k * 3 + 2].boneWeights.Z = 0;
                        Groups[i].vertices[k * 3 + 2].boneWeights.W = 0;
                    }
                    Groups[i].OriginalVertices[k * 3 + 0] = Groups[i].vertices[k * 3 + 0];
                    Groups[i].OriginalVertices[k * 3 + 1] = Groups[i].vertices[k * 3 + 1];
                    Groups[i].OriginalVertices[k * 3 + 2] = Groups[i].vertices[k * 3 + 2];
                }
            }
        }

        private void SetupJoints()
        {
            for (int i = 0; i < numJoints; i++)
            {
                Joints[i].parentIndex = FindJointByName(Joints[i].parentNameS);
            }
            for (int i = 0; i < numJoints; i++)
            {
                Vector4 quat = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);



                Vector3 tmp = new Vector3(Joints[i].rotation[0], Joints[i].rotation[1], Joints[i].rotation[2]);
                Joints[i].matLocalSkeleton = new MilkshapeMatrix3x4();

                //AngleMatrix(tmp, Joints[i].matLocalSkeleton);
                //这两种方式差不多是一样的
                quat = AngleQuaternion(tmp);
                QuaternionMatrix(quat, Joints[i].matLocalSkeleton);


                Joints[i].matLocalSkeleton.v[0][3] = Joints[i].position[0];
                Joints[i].matLocalSkeleton.v[1][3] = Joints[i].position[1];
                Joints[i].matLocalSkeleton.v[2][3] = Joints[i].position[2];

                if (Joints[i].parentIndex == -1)
                {
                    Joints[i].matGlobalSkeleton = new MilkshapeMatrix3x4();
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 4; l++)
                            Joints[i].matGlobalSkeleton.v[k][l] = Joints[i].matLocalSkeleton.v[k][l];
                }
                else
                {
                    Joints[i].matGlobalSkeleton = new MilkshapeMatrix3x4();
                    R_ConcatTransforms(Joints[Joints[i].parentIndex].matGlobalSkeleton, Joints[i].matLocalSkeleton, Joints[i].matGlobalSkeleton);
                }

                SetupTangents();
            }


        }

        public void TestRender(float delta, float scale)
        {
            for (int i = 0; i < numGroups; i++)
            {
                for (int k = 0; k < Groups[i].numTriangles; k++)
                {
                    GL.glBegin(GL.GL_TRIANGLES);
                    if (i == 0)
                    {
                        GL.glColor3f(1.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        GL.glColor3f(0.0f, 1.0f, 0.0f);
                    }

                    GL.glNormal3f(Groups[i].OriginalVertices[k * 3 + 0].Normal.X, Groups[i].vertices[k * 3 + 0].Normal.Y, Groups[i].vertices[k * 3 + 0].Normal.Z);
                    GL.glVertex3f(Groups[i].OriginalVertices[k * 3 + 0].Position.X * scale, Groups[i].OriginalVertices[k * 3 + 0].Position.Y * scale + delta, Groups[i].OriginalVertices[k * 3 + 0].Position.Z * scale);
                    GL.glVertex3f(Groups[i].OriginalVertices[k * 3 + 1].Position.X * scale, Groups[i].OriginalVertices[k * 3 + 1].Position.Y * scale + delta, Groups[i].OriginalVertices[k * 3 + 1].Position.Z * scale);
                    GL.glVertex3f(Groups[i].OriginalVertices[k * 3 + 2].Position.X * scale, Groups[i].OriginalVertices[k * 3 + 2].Position.Y * scale + delta, Groups[i].OriginalVertices[k * 3 + 2].Position.Z * scale);
                    GL.glEnd();
                }
            }
        }

        public void TestRenderEx(float scale)
        {
            for (int i = 0; i < numGroups; i++)
            {
                for (int k = 0; k < Groups[i].numTriangles; k++)
                {
                    GL.glBegin(GL.GL_TRIANGLES);
                    if (i == 0)
                    {
                        GL.glColor3f(1.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        GL.glColor3f(0.0f, 1.0f, 0.0f);
                    }
                    GL.glNormal3f(Groups[i].vertices[k * 3 + 0].Normal.X, Groups[i].vertices[k * 3 + 0].Normal.Y, Groups[i].vertices[k * 3 + 0].Normal.Z);
                    GL.glVertex3f(Groups[i].vertices[k * 3 + 0].Position.X * scale, Groups[i].vertices[k * 3 + 0].Position.Y * scale, Groups[i].vertices[k * 3 + 0].Position.Z * scale);
                    GL.glVertex3f(Groups[i].vertices[k * 3 + 1].Position.X * scale, Groups[i].vertices[k * 3 + 1].Position.Y * scale, Groups[i].vertices[k * 3 + 1].Position.Z * scale);
                    GL.glVertex3f(Groups[i].vertices[k * 3 + 2].Position.X * scale, Groups[i].vertices[k * 3 + 2].Position.Y * scale, Groups[i].vertices[k * 3 + 2].Position.Z * scale);
                    
                    GL.glEnd();
                }
            }
        }

        Vector4 XW_B(float Rool_angle0, float Pitch_angle0, float Yaw_angle0)
        {
            Vector4 XW_QuarterN = new Vector4();
            double P = (double)Pitch_angle0 * 3.1415926f / 180f;//弧度 单位
            double R = (double)Rool_angle0 * 3.1415926f / 180f;
            double Y = (double)Yaw_angle0 * 3.1415926f / 180f;

            P /= 2;
            R /= 2;
            Y /= 2;


            XW_QuarterN.W = (float)(System.Math.Cos(R) * System.Math.Cos(P) * System.Math.Cos(Y) + System.Math.Sin(R) * System.Math.Sin(P) * System.Math.Sin(Y));

            XW_QuarterN.X = (float)(System.Math.Sin(R) * System.Math.Cos(P) * System.Math.Cos(Y) - System.Math.Cos(R) * System.Math.Sin(P) * System.Math.Sin(Y));

            XW_QuarterN.Y = (float)(System.Math.Cos(R) * System.Math.Sin(P) * System.Math.Cos(Y) + System.Math.Sin(R) * System.Math.Cos(P) * System.Math.Sin(Y));

            XW_QuarterN.Z = (float)(System.Math.Cos(R) * System.Math.Cos(P) * System.Math.Sin(Y) - System.Math.Sin(R) * System.Math.Sin(P) * System.Math.Cos(Y));


            //XW_QuarterN.q_w = System.Math.Acos(XW_QuarterN.W) * 2 * 180 / 3.1415926; //ysj++ 

            return XW_QuarterN;

        }

        float q0 = 1, q1 = 1, q2 = 1, q3 = 1;
        float q_w = 0;
        void IMU(float gx, float gy, float gz)
        {
            float norm;

            float halfT = 0.05f;

            // 更新四元数
            q0 = q0 + (-q1 * gx - q2 * gy - q3 * gz) * halfT;
            q1 = q1 + (q0 * gx + q2 * gz - q3 * gy) * halfT;
            q2 = q2 + (q0 * gy - q1 * gz + q3 * gx) * halfT;
            q3 = q3 + (q0 * gz + q1 * gy - q2 * gx) * halfT;

            //四元数规范化
            norm = (float)System.Math.Sqrt(q0 * q0 + q1 * q1 + q2 * q2 + q3 * q3);
            q0 = q0 / norm;
            q1 = q1 / norm;
            q2 = q2 / norm;
            q3 = q3 / norm;

            q_w = (float)System.Math.Acos((double)(q0)) * 2 * 180 / 3.1415926f;
        }

        /***********************共轭求逆**************************/
        Vector4 Conjugate_Q(Vector4 QN)
        {
            Vector4 QN_C = new Vector4();//共轭
            float norm = (float)System.Math.Sqrt((double)(QN.X * QN.X + QN.Y * QN.Y + QN.Z * QN.Z + QN.W * QN.W));//求模

            QN_C.W = QN.W;
            QN_C.X = -QN.X;
            QN_C.Y = -QN.Y;
            QN_C.Z = -QN.Z;
            

            Vector4 QN_I = new Vector4();//求逆

            QN_I.X = QN_C.X / norm;
            QN_I.Y = QN_C.Y / norm;
            QN_I.Z = QN_C.Z / norm;
            QN_I.W = QN_C.W / norm;

            return QN_I;
        }
        
        Vector4 MUL_Q(Vector4 Q1, Vector4 Q2)
        {         
            float p1 = Q1.X;
            float p2 = Q1.Y;
            float p3 = Q1.Z;
            float lm = Q1.W;

            MilkshapeMatrix4x4 A1 = new MilkshapeMatrix4x4();
            A1.v[0][0] = lm;
            A1.v[0][1] = -p1;
            A1.v[0][2] = -p2;
            A1.v[0][3] = -p3;

            A1.v[1][0] = p1;
            A1.v[1][1] = lm;
            A1.v[1][2] = -p3;
            A1.v[1][3] = p2;

            A1.v[2][0] = p2;
            A1.v[2][1] = p3;
            A1.v[2][2] = lm;
            A1.v[2][3] = -p1;

            A1.v[3][0] = p3;
            A1.v[3][1] = -p2;
            A1.v[3][2] = p1;
            A1.v[3][3] = lm;


            //Q2 和矩阵A1 相乘
            Vector4 Qnew = new Vector4();
            Qnew.W = Q2.W * lm + Q2.X * -p1 + Q2.Y * -p2 + Q2.Z * -p3;
            Qnew.X = Q2.W * p1 + Q2.X * lm + Q2.Y * -p3 + Q2.Z * p2;
            Qnew.Y = Q2.W * p2 + Q2.X * p3 + Q2.Y * lm + Q2.Z * -p1;
            Qnew.Z = Q2.W * p3 + Q2.X * -p2 + Q2.Y * p1 + Q2.Z * lm;

            return Qnew;


        }


        void UpdateAllNode(string  data)
        {
            //根据传感器发送来的数据，改变joint中的数值
            Tokens f = new Tokens(data, new char[] { ' ', '\t', '|' });

            //numJoints  关节点个数

            //pos
            Joints[0].position[0] = float.Parse(f.elements[0]); //f.elements 值得顺序貌似是xyz
            Joints[0].position[1] = float.Parse(f.elements[1]);
            Joints[0].position[2] = float.Parse(f.elements[2]);
            //rot  yxz
            Joints[0].rotation[0] = float.Parse(f.elements[4]);
            Joints[0].rotation[1] = float.Parse(f.elements[3]);
            Joints[0].rotation[2] = float.Parse(f.elements[5]);

            Joints[1].rotation[0] = float.Parse(f.elements[7]);
            Joints[1].rotation[1] = float.Parse(f.elements[6]);
            Joints[1].rotation[2] = float.Parse(f.elements[8]);

            Joints[2].rotation[0] = float.Parse(f.elements[10]);
            Joints[2].rotation[1] = float.Parse(f.elements[9]);
            Joints[2].rotation[2] = float.Parse(f.elements[11]);

            Joints[3].rotation[0] = float.Parse(f.elements[13]);
            Joints[3].rotation[1] = float.Parse(f.elements[12]);
            Joints[3].rotation[2] = float.Parse(f.elements[14]);

            Joints[4].rotation[0] = float.Parse(f.elements[16]);
            Joints[4].rotation[1] = float.Parse(f.elements[15]);
            Joints[4].rotation[2] = float.Parse(f.elements[17]);

            Joints[5].rotation[0] = float.Parse(f.elements[19]);
            Joints[5].rotation[1] = float.Parse(f.elements[18]);
            Joints[5].rotation[2] = float.Parse(f.elements[20]);

            Joints[6].rotation[0] = float.Parse(f.elements[22]);
            Joints[6].rotation[1] = float.Parse(f.elements[21]);
            Joints[6].rotation[2] = float.Parse(f.elements[23]);

            Joints[7].rotation[0] = float.Parse(f.elements[25]);
            Joints[7].rotation[1] = float.Parse(f.elements[24]);
            Joints[7].rotation[2] = float.Parse(f.elements[26]);

            Joints[8].rotation[0] = float.Parse(f.elements[28]);
            Joints[8].rotation[1] = float.Parse(f.elements[27]);
            Joints[8].rotation[2] = float.Parse(f.elements[29]);

            Joints[9].rotation[0] = float.Parse(f.elements[31]);
            Joints[9].rotation[1] = float.Parse(f.elements[30]);
            Joints[9].rotation[2] = float.Parse(f.elements[32]);

            Joints[10].rotation[0] = float.Parse(f.elements[34]);
            Joints[10].rotation[1] = float.Parse(f.elements[33]);
            Joints[10].rotation[2] = float.Parse(f.elements[35]);

            Joints[11].rotation[0] = float.Parse(f.elements[37]);
            Joints[11].rotation[1] = float.Parse(f.elements[36]);
            Joints[11].rotation[2] = float.Parse(f.elements[38]);

            Joints[12].rotation[0] = float.Parse(f.elements[40]);
            Joints[12].rotation[1] = float.Parse(f.elements[39]);
            Joints[12].rotation[2] = float.Parse(f.elements[41]);

            Joints[13].rotation[0] = float.Parse(f.elements[43]);
            Joints[13].rotation[1] = float.Parse(f.elements[42]);
            Joints[13].rotation[2] = float.Parse(f.elements[44]);

            Joints[14].rotation[0] = float.Parse(f.elements[46]);
            Joints[14].rotation[1] = float.Parse(f.elements[45]);
            Joints[14].rotation[2] = float.Parse(f.elements[47]);

            Joints[15].rotation[0] = float.Parse(f.elements[49]);
            Joints[15].rotation[1] = float.Parse(f.elements[48]);
            Joints[15].rotation[2] = float.Parse(f.elements[50]);

            Joints[16].rotation[0] = float.Parse(f.elements[52]);
            Joints[16].rotation[1] = float.Parse(f.elements[51]);
            Joints[16].rotation[2] = float.Parse(f.elements[53]);

            Joints[17].rotation[0] = float.Parse(f.elements[55]);
            Joints[17].rotation[1] = float.Parse(f.elements[54]);
            Joints[17].rotation[2] = float.Parse(f.elements[56]);

            Joints[18].rotation[0] = float.Parse(f.elements[58]);
            Joints[18].rotation[1] = float.Parse(f.elements[57]);
            Joints[18].rotation[2] = float.Parse(f.elements[59]);



            for (int i = 0; i < SegmentCollection.arraySegment.Length;i++ )
            {

            }
        }

        void updatevertex(Vector3 angles,int boneid)
        {            
            //Vector4 QHH = XW_B(Rool_OlA, Pitch_OlA, Yaw_OlA);  //等同于angleQuaternion  函数作用相同
            Vector4 QHH = AngleQuaternion(angles);
            Vector4 QX0W_N = new Vector4();
            QX0W_N = Conjugate_Q(QHH);            
            for (int i = 0; i < numGroups; i++)
            {
                for (int j = 0; j < Groups[i].numTriangles; j++)
                {
                    if (Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].boneId != boneid)
                    {
                        continue;
                    }
                    //三角形的第一个点
                    Vector4 Q_Point000 = new Vector4();
                    Q_Point000.X = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[0];                    
                    Q_Point000.Y = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[1];
                    Q_Point000.Z = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[2];
                    Q_Point000.W = 0;

                    Q_Point000.X = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[0];
                    Q_Point000.Y = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[1];
                    Q_Point000.Z = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[2];
                    Q_Point000.W = 0;



                    Vector4 Q001 = MUL_Q(QHH, Q_Point000);
                    Vector4 Q002 = MUL_Q(Q001, QX0W_N);

                    //Q022.X; //这是计算后的新的坐标点
                    Groups[i].vertices[j * 3 + 0].Position.X = Q002.X;
                    Groups[i].vertices[j * 3 + 0].Position.Y = Q002.Y;
                    Groups[i].vertices[j * 3 + 0].Position.Z = Q002.Z;

                    //三角形的第二个点
                    Vector4 Q_Point001 = new Vector4();
                    Q_Point001.X = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[1]].vertex[0];
                    Q_Point001.Y = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[1]].vertex[1];
                    Q_Point001.Z = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[1]].vertex[2];
                    Q_Point001.W = 0;

                    Vector4 Q011 = MUL_Q(QHH, Q_Point001);
                    Vector4 Q012 = MUL_Q(Q011, QX0W_N);

                    //Q022.X; //这是计算后的新的坐标点
                    Groups[i].vertices[j * 3 + 1].Position.X = Q012.X;
                    Groups[i].vertices[j * 3 + 1].Position.Y = Q012.Y;
                    Groups[i].vertices[j * 3 + 1].Position.Z = Q012.Z;

                    //三角形的第三个点
                    Vector4 Q_Point00 = new Vector4();
                    Q_Point00.X = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[2]].vertex[0];
                    Q_Point00.Y = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[2]].vertex[1];
                    Q_Point00.Z = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[2]].vertex[2];
                    Q_Point00.W = 0;

                    Vector4 Q021 = MUL_Q(QHH, Q_Point00);
                    Vector4 Q022 = MUL_Q(Q021, QX0W_N);

                    //Q022.X; //这是计算后的新的坐标点
                    Groups[i].vertices[j * 3 + 2].Position.X = Q022.X;
                    Groups[i].vertices[j * 3 + 2].Position.Y = Q022.Y;
                    Groups[i].vertices[j * 3 + 2].Position.Z = Q022.Z;


                }
            }

        }

        void updatevertexEXXXX(Vector3 angles, int boneid)
        {
            Vector4 QHH = AngleQuaternion(angles);
            Vector4 QX0W_N = new Vector4();
            QX0W_N = Conjugate_Q(QHH);
            for (int i = 0; i < numGroups; i++)
            {
                for (int j = 0; j < Groups[i].numTriangles; j++)
                {
                    if (Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].boneId != boneid)
                    {
                        continue;
                    }
                    //三角形的第一个点
                    Vector4 Q_Point000 = new Vector4();
                    Q_Point000.X = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[0];
                    Q_Point000.Y = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[1];
                    Q_Point000.Z = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[0]].vertex[2];
                    Q_Point000.W = 0;

                    Vector4 Q001 = MUL_Q(QHH, Q_Point000);
                    Vector4 Q002 = MUL_Q(Q001, QX0W_N);

                    //Q022.X; //这是计算后的新的坐标点
                    Groups[i].vertices[j * 3 + 0].Position.X = Q002.X;
                    Groups[i].vertices[j * 3 + 0].Position.Y = Q002.Y;
                    Groups[i].vertices[j * 3 + 0].Position.Z = Q002.Z;

                    //三角形的第二个点
                    Vector4 Q_Point001 = new Vector4();
                    Q_Point001.X = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[1]].vertex[0];
                    Q_Point001.Y = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[1]].vertex[1];
                    Q_Point001.Z = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[1]].vertex[2];
                    Q_Point001.W = 0;

                    Vector4 Q011 = MUL_Q(QHH, Q_Point001);
                    Vector4 Q012 = MUL_Q(Q011, QX0W_N);

                    //Q022.X; //这是计算后的新的坐标点
                    Groups[i].vertices[j * 3 + 1].Position.X = Q012.X;
                    Groups[i].vertices[j * 3 + 1].Position.Y = Q012.Y;
                    Groups[i].vertices[j * 3 + 1].Position.Z = Q012.Z;

                    //三角形的第三个点
                    Vector4 Q_Point00 = new Vector4();
                    Q_Point00.X = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[2]].vertex[0];
                    Q_Point00.Y = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[2]].vertex[1];
                    Q_Point00.Z = Vertices[Triangles[Groups[i].triangleIndices[j]].vertexIndices[2]].vertex[2];
                    Q_Point00.W = 0;

                    Vector4 Q021 = MUL_Q(QHH, Q_Point00);
                    Vector4 Q022 = MUL_Q(Q021, QX0W_N);

                    //Q022.X; //这是计算后的新的坐标点
                    Groups[i].vertices[j * 3 + 2].Position.X = Q022.X;
                    Groups[i].vertices[j * 3 + 2].Position.Y = Q022.Y;
                    Groups[i].vertices[j * 3 + 2].Position.Z = Q022.Z;


                }
            }

        }

        private void R_ConcatTransforms(MilkshapeMatrix3x4 in1, MilkshapeMatrix3x4 in2, MilkshapeMatrix3x4 o)
        {
            o.v[0][0] = in1.v[0][0] * in2.v[0][0] + in1.v[0][1] * in2.v[1][0] +
                in1.v[0][2] * in2.v[2][0];
            o.v[0][1] = in1.v[0][0] * in2.v[0][1] + in1.v[0][1] * in2.v[1][1] +
                in1.v[0][2] * in2.v[2][1];
            o.v[0][2] = in1.v[0][0] * in2.v[0][2] + in1.v[0][1] * in2.v[1][2] +
                in1.v[0][2] * in2.v[2][2];
            o.v[0][3] = in1.v[0][0] * in2.v[0][3] + in1.v[0][1] * in2.v[1][3] +
                in1.v[0][2] * in2.v[2][3] + in1.v[0][3];
            o.v[1][0] = in1.v[1][0] * in2.v[0][0] + in1.v[1][1] * in2.v[1][0] +
                in1.v[1][2] * in2.v[2][0];
            o.v[1][1] = in1.v[1][0] * in2.v[0][1] + in1.v[1][1] * in2.v[1][1] +
                in1.v[1][2] * in2.v[2][1];
            o.v[1][2] = in1.v[1][0] * in2.v[0][2] + in1.v[1][1] * in2.v[1][2] +
                in1.v[1][2] * in2.v[2][2];
            o.v[1][3] = in1.v[1][0] * in2.v[0][3] + in1.v[1][1] * in2.v[1][3] +
                in1.v[1][2] * in2.v[2][3] + in1.v[1][3];
            o.v[2][0] = in1.v[2][0] * in2.v[0][0] + in1.v[2][1] * in2.v[1][0] +
                in1.v[2][2] * in2.v[2][0];
            o.v[2][1] = in1.v[2][0] * in2.v[0][1] + in1.v[2][1] * in2.v[1][1] +
                in1.v[2][2] * in2.v[2][1];
            o.v[2][2] = in1.v[2][0] * in2.v[0][2] + in1.v[2][1] * in2.v[1][2] +
                in1.v[2][2] * in2.v[2][2];
            o.v[2][3] = in1.v[2][0] * in2.v[0][3] + in1.v[2][1] * in2.v[1][3] +
                in1.v[2][2] * in2.v[2][3] + in1.v[2][3];
        }
       
        private Vector4 QuaternionSlerp(Vector4 p, Vector4 q, float t)
        {
            float omega, cosom, sinom, sclp, sclq;
            Vector4 qt = new Vector4();
            // decide if one of the quaternions is backwards
            float a = 0;
            float b = 0;
            a += (float)Math.Pow(p.X - q.X, 2);
            a += (float)Math.Pow(p.Y - q.Y, 2);
            a += (float)Math.Pow(p.Z - q.Z, 2);
            a += (float)Math.Pow(p.W - q.W, 2);
            b += (float)Math.Pow(p.X + q.X, 2);
            b += (float)Math.Pow(p.Y + q.Y, 2);
            b += (float)Math.Pow(p.Z + q.Z, 2);
            b += (float)Math.Pow(p.W + q.W, 2);
            if (a > b)
            {
                q.X = -q.X;
                q.Y = -q.Y;
                q.Z = -q.Z;
                q.W = -q.W;
            }

            cosom = p.X * q.X + p.Y * q.Y + p.Z * q.Z + p.W * q.W;

            if ((1.0 + cosom) > 0.00000001)
            {
                if ((1.0 - cosom) > 0.00000001)
                {
                    omega = (float)Math.Acos(cosom);
                    sinom = (float)Math.Sin(omega);
                    sclp = (float)Math.Sin((1.0 - t) * omega) / sinom;
                    sclq = (float)Math.Sin(t * omega) / sinom;
                }
                else
                {
                    sclp = 1.0f - t;
                    sclq = t;
                }
                qt.X = sclp * p.X + sclq * q.X;
                qt.Y = sclp * p.Y + sclq * q.Y;
                qt.Z = sclp * p.Z + sclq * q.Z;
                qt.W = sclp * p.W + sclq * q.W;
            }
            else
            {
                qt.X = -p.Y;
                qt.Y = p.X;
                qt.Z = -p.W;
                qt.W = p.Z;
                sclp = (float)Math.Sin((1.0 - t) * 0.5 * Math.PI);
                sclq = (float)Math.Sin(t * 0.5 * Math.PI);
                qt.X = sclp * p.X + sclq * qt.X;
                qt.Y = sclp * p.Y + sclq * qt.Y;
                qt.Z = sclp * p.Z + sclq * qt.Z;
            }
            return qt;
        }

        private Vector4 AngleQuaternion(Vector3 angles)
        {
            float angle;
            float sr, sp, sy, cr, cp, cy;
            Vector4 quaternion = new Vector4();
            // FIXME: rescale the inputs to 1/2 angle
            angle = (float)angles.Z * 0.5f;
            sy = (float)Math.Sin(angle);
            cy = (float)Math.Cos(angle);
            angle = (float)angles.Y * 0.5f;
            sp = (float)Math.Sin(angle);
            cp = (float)Math.Cos(angle);
            angle = (float)angles.X * 0.5f;
            sr = (float)Math.Sin(angle);
            cr = (float)Math.Cos(angle);

            quaternion.X = sr * cp * cy - cr * sp * sy; // X
            quaternion.Y = cr * sp * cy + sr * cp * sy; // Y
            quaternion.Z = cr * cp * sy - sr * sp * cy; // Z
            quaternion.W = cr * cp * cy + sr * sp * sy; // W
            return quaternion;
        }
        private void QuaternionMatrix(Vector4 quaternion, MilkshapeMatrix3x4 matrix /*float (*matrix)[4] */)
        {
            matrix.v[0][0] = (float)(1.0f - 2.0 * quaternion.Y * quaternion.Y - 2.0 * quaternion.Z * quaternion.Z);
            matrix.v[1][0] = (float)(2.0f * quaternion.X * quaternion.Y + 2.0 * quaternion.W * quaternion.Z);
            matrix.v[2][0] = (float)(2.0f * quaternion.X * quaternion.Z - 2.0 * quaternion.W * quaternion.Y);

            matrix.v[0][1] = (float)(2.0f * quaternion.X * quaternion.Y - 2.0 * quaternion.W * quaternion.Z);
            matrix.v[1][1] = (float)(1.0f - 2.0 * quaternion.X * quaternion.X - 2.0 * quaternion.Z * quaternion.Z);
            matrix.v[2][1] = (float)(2.0f * quaternion.Y * quaternion.Z + 2.0 * quaternion.W * quaternion.X);

            matrix.v[0][2] = (float)(2.0f * quaternion.X * quaternion.Z + 2.0 * quaternion.W * quaternion.Y);
            matrix.v[1][2] = (float)(2.0f * quaternion.Y * quaternion.Z - 2.0 * quaternion.W * quaternion.X);
            matrix.v[2][2] = (float)(1.0f - 2.0 * quaternion.X * quaternion.X - 2.0 * quaternion.Y * quaternion.Y);
        }

        private int FindJointByName(String name)
        {
            for (int i = 0; i < numJoints; i++)
                if (Joints[i].nameS == name)
                    return i;
            return -1;
        }
        
        private void AngleMatrix(Vector3 angles, MilkshapeMatrix3x4 matrix)
        {
            float angle;
            float sr, sp, sy, cr, cp, cy;

            angle = angles.Z;
            sy = (float)Math.Sin(angle);
            cy = (float)Math.Cos(angle);
            angle = angles.Y;
            sp = (float)Math.Sin(angle);
            cp = (float)Math.Cos(angle);
            angle = angles.X;
            sr = (float)Math.Sin(angle);
            cr = (float)Math.Cos(angle);

            // matrix = (Z * Y) * X
            matrix.v[0][0] = cp * cy;
            matrix.v[1][0] = cp * sy;
            matrix.v[2][0] = -sp;
            matrix.v[0][1] = sr * sp * cy + cr * -sy;
            matrix.v[1][1] = sr * sp * sy + cr * cy;
            matrix.v[2][1] = sr * cp;
            matrix.v[0][2] = (cr * sp * cy + -sr * -sy);
            matrix.v[1][2] = (cr * sp * sy + -sr * cy);
            matrix.v[2][2] = cr * cp;
            matrix.v[0][3] = 0.0f;
            matrix.v[1][3] = 0.0f;
            matrix.v[2][3] = 0.0f;
        }
        
        
        
        private void SetupTangents()
        {
            for (int j = 0; j < numJoints; j++)
            {
                MilkshapeJoint joint = Joints[j];
                int numPositionKeys = joint.numPosKeyFrames;
                joint.tangents = new MilkshapeTangent[numPositionKeys];

                // clear all tangents (zero derivatives)
                for (int k = 0; k < numPositionKeys; k++)
                {
                    joint.tangents[k] = new MilkshapeTangent();
                    joint.tangents[k].tangentIn.X = 0.0f;
                    joint.tangents[k].tangentIn.Y = 0.0f;
                    joint.tangents[k].tangentIn.Z = 0.0f;
                    joint.tangents[k].tangentOut.X = 0.0f;
                    joint.tangents[k].tangentOut.Y = 0.0f;
                    joint.tangents[k].tangentOut.Z = 0.0f;
                }

                // if there are more than 2 keys, we can calculate tangents, otherwise we use zero derivatives
                if (numPositionKeys > 2)
                {
                    for (int k = 0; k < numPositionKeys; k++)
                    {
                        // make the curve tangents looped
                        int k0 = k - 1;
                        if (k0 < 0)
                            k0 = numPositionKeys - 1;
                        int k1 = k;
                        int k2 = k + 1;
                        if (k2 >= numPositionKeys)
                            k2 = 0;
                        // calculate the tangent, which is the vector from key[k - 1] to key[k + 1]
                        float[] tangent = new float[3];
                        tangent[0] = (joint.posKeyFrames[k2].position[0] - joint.posKeyFrames[k0].position[0]);
                        tangent[1] = (joint.posKeyFrames[k2].position[1] - joint.posKeyFrames[k0].position[1]);
                        tangent[2] = (joint.posKeyFrames[k2].position[2] - joint.posKeyFrames[k0].position[2]);

                        // weight the incoming and outgoing tangent by their time to avoid changes in speed, if the keys are not within the same interval
                        float dt1 = joint.posKeyFrames[k1].time - joint.posKeyFrames[k0].time;
                        float dt2 = joint.posKeyFrames[k2].time - joint.posKeyFrames[k1].time;
                        float dt = dt1 + dt2;
                        joint.tangents[k1].tangentIn.X = tangent[0] * dt1 / dt;
                        joint.tangents[k1].tangentIn.Y = tangent[1] * dt1 / dt;
                        joint.tangents[k1].tangentIn.Z = tangent[2] * dt1 / dt;

                        joint.tangents[k1].tangentOut.X = tangent[0] * dt2 / dt;
                        joint.tangents[k1].tangentOut.Y = tangent[1] * dt2 / dt;
                        joint.tangents[k1].tangentOut.Z = tangent[2] * dt2 / dt;
                    }
                }
            }

        }

        //主要作用是更新current 的两个矩阵
        private void rebuildJoint(MilkshapeJoint joint)
        {
            //
            // calculate joint animation matrix, this matrix will animate matLocalSkeleton
            //
            float frame = currentTime;
            //float frame=15;
            Vector3 pos = new Vector3(0f, 0f, 0f);
            int numPositionKeys = joint.numPosKeyFrames;
            if (numPositionKeys > 0)
            {

                int i1 = -1;
                int i2 = -1;

                // find the two keys, where "frame" is in between for the position channel
                for (int i = 0; i < (numPositionKeys - 1); i++)
                {
                    if (frame >= joint.posKeyFrames[i].time && frame < joint.posKeyFrames[i + 1].time)
                    {
                        i1 = i;
                        i2 = i + 1;
                        break;
                    }
                }

                // if there are no such keys
                if (i1 == -1 || i2 == -1)
                {
                    // either take the first
                    if (frame < joint.posKeyFrames[0].time)
                    {
                        pos.X = joint.posKeyFrames[0].position[0];
                        pos.Y = joint.posKeyFrames[0].position[1];
                        pos.Z = joint.posKeyFrames[0].position[2];
                    }

                    // or the last key
                    else if (frame >= joint.posKeyFrames[numPositionKeys - 1].time)
                    {
                        pos.X = joint.posKeyFrames[numPositionKeys - 1].position[0];
                        pos.Y = joint.posKeyFrames[numPositionKeys - 1].position[1];
                        pos.Z = joint.posKeyFrames[numPositionKeys - 1].position[2];
                    }
                }

                // there are such keys, so interpolate using hermite interpolation
                else
                {
                    MilkshapePositionKey p0 = joint.posKeyFrames[i1];
                    MilkshapePositionKey p1 = joint.posKeyFrames[i2];
                    MilkshapeTangent m0 = joint.tangents[i1];
                    MilkshapeTangent m1 = joint.tangents[i2];

                    // normalize the time between the keys into [0..1]
                    float t = (frame - joint.posKeyFrames[i1].time) / (joint.posKeyFrames[i2].time - joint.posKeyFrames[i1].time);
                    float t2 = t * t;
                    float t3 = t2 * t;

                    // calculate hermite basis
                    float h1 = 2.0f * t3 - 3.0f * t2 + 1.0f;
                    float h2 = -2.0f * t3 + 3.0f * t2;
                    float h3 = t3 - 2.0f * t2 + t;
                    float h4 = t3 - t2;

                    // do hermite interpolation
                    pos.X = h1 * p0.position[0] + h3 * m0.tangentOut.X + h2 * p1.position[0] + h4 * m1.tangentIn.X;
                    pos.Y = h1 * p0.position[1] + h3 * m0.tangentOut.Y + h2 * p1.position[1] + h4 * m1.tangentIn.Y;
                    pos.Z = h1 * p0.position[2] + h3 * m0.tangentOut.Z + h2 * p1.position[2] + h4 * m1.tangentIn.Z;
                }
            }

            Vector4 quat = new Vector4(0f, 0f, 0f, 1f);
            int numRotationKeys = (int)joint.numRotKeyFrames;
            if (numRotationKeys > 0)
            {
                int i1 = -1;
                int i2 = -1;

                // find the two keys, where "frame" is in between for the rotation channel
                for (int i = 0; i < (numRotationKeys - 1); i++)
                {
                    if (frame >= joint.rotKeyFrames[i].time && frame < joint.rotKeyFrames[i + 1].time)
                    {
                        i1 = i;
                        i2 = i + 1;
                        break;
                    }
                }

                // if there are no such keys
                if (i1 == -1 || i2 == -1)
                {
                    // either take the first key
                    if (frame < joint.rotKeyFrames[0].time)
                    {
                        Vector3 tmp = new Vector3(joint.rotKeyFrames[0].rotation[0], joint.rotKeyFrames[0].rotation[1], joint.rotKeyFrames[0].rotation[2]);
                        quat = AngleQuaternion(tmp);
                    }

                    // or the last key
                    else if (frame >= joint.rotKeyFrames[numRotationKeys - 1].time)
                    {
                        Vector3 tmp = new Vector3(joint.rotKeyFrames[numRotationKeys - 1].rotation[0], joint.rotKeyFrames[numRotationKeys - 1].rotation[1], joint.rotKeyFrames[numRotationKeys - 1].rotation[2]);
                        quat = AngleQuaternion(tmp);
                    }
                }

                // there are such keys, so do the quaternion slerp interpolation
                else
                {
                    float t = (frame - joint.rotKeyFrames[i1].time) / (joint.rotKeyFrames[i2].time - joint.rotKeyFrames[i1].time);
                    Vector4 q1 = new Vector4();
                    Vector3 tmp = new Vector3(joint.rotKeyFrames[i1].rotation[0], joint.rotKeyFrames[i1].rotation[1], joint.rotKeyFrames[i1].rotation[2]);
                    q1 = AngleQuaternion(tmp);
                    Vector4 q2 = new Vector4();
                    tmp = new Vector3(joint.rotKeyFrames[i2].rotation[0], joint.rotKeyFrames[i2].rotation[1], joint.rotKeyFrames[i2].rotation[2]);
                    q2 = AngleQuaternion(tmp);
                    quat = QuaternionSlerp(q1, q2, t);
                }
            }

            // make a matrix from pos/quat
            //可否根据矩阵得到

            MilkshapeMatrix3x4 matAnimate = new MilkshapeMatrix3x4();
            QuaternionMatrix(quat, matAnimate);
            matAnimate.v[0][3] = pos.X;
            matAnimate.v[1][3] = pos.Y;
            matAnimate.v[2][3] = pos.Z;

            // animate the local joint matrix using: matLocal = matLocalSkeleton * matAnimate
            if (joint.matLocal == null)
                joint.matLocal = new MilkshapeMatrix3x4();
            R_ConcatTransforms(joint.matLocalSkeleton, matAnimate, joint.matLocal);

            // build up the hierarchy if joints
            if (joint.parentIndex == -1)
            {
                if (joint.matGlobal == null)
                    joint.matGlobal = new MilkshapeMatrix3x4();
                for (int k = 0; k < 3; k++)
                    for (int l = 0; l < 4; l++)
                        joint.matGlobal.v[k][l] = joint.matLocal.v[k][l];
            }
            else
            {
                MilkshapeJoint parentJoint = Joints[joint.parentIndex];
                if (joint.matGlobal == null)
                    joint.matGlobal = new MilkshapeMatrix3x4();
                if (parentJoint.matGlobal == null)
                    parentJoint.matGlobal = new MilkshapeMatrix3x4();
                R_ConcatTransforms(parentJoint.matGlobal, joint.matLocal, joint.matGlobal);
            }

        }

        public void rebuildJoint(string data)
        {
            //根据传感器发送来的数据，改变joint中的数值
            Tokens f = new Tokens(data, new char[] { ' ', '\t', '|' });

            //pos
            Joints[0].position[0] = float.Parse(f.elements[0]); //f.elements 值得顺序貌似是xyz
            Joints[0].position[1] = float.Parse(f.elements[1]);
            Joints[0].position[2] = float.Parse(f.elements[2]);
            //rot  yxz
            Joints[0].rotation[0] = float.Parse(f.elements[4]);
            Joints[0].rotation[1] = float.Parse(f.elements[3]);
            Joints[0].rotation[2] = float.Parse(f.elements[5]);

            Joints[1].rotation[0] = float.Parse(f.elements[7]);
            Joints[1].rotation[1] = float.Parse(f.elements[6]);
            Joints[1].rotation[2] = float.Parse(f.elements[8]);

            Joints[2].rotation[0] = float.Parse(f.elements[10]);
            Joints[2].rotation[1] = float.Parse(f.elements[9]);
            Joints[2].rotation[2] = float.Parse(f.elements[11]);

            Joints[3].rotation[0] = float.Parse(f.elements[13]);
            Joints[3].rotation[1] = float.Parse(f.elements[12]);
            Joints[3].rotation[2] = float.Parse(f.elements[14]);

            Joints[4].rotation[0] = float.Parse(f.elements[16]);
            Joints[4].rotation[1] = float.Parse(f.elements[15]);
            Joints[4].rotation[2] = float.Parse(f.elements[17]);

            Joints[5].rotation[0] = float.Parse(f.elements[19]);
            Joints[5].rotation[1] = float.Parse(f.elements[18]);
            Joints[5].rotation[2] = float.Parse(f.elements[20]);

            Joints[6].rotation[0] = float.Parse(f.elements[22]);
            Joints[6].rotation[1] = float.Parse(f.elements[21]);
            Joints[6].rotation[2] = float.Parse(f.elements[23]);

            Joints[7].rotation[0] = float.Parse(f.elements[25]);
            Joints[7].rotation[1] = float.Parse(f.elements[24]);
            Joints[7].rotation[2] = float.Parse(f.elements[26]);

            Joints[8].rotation[0] = float.Parse(f.elements[28]);
            Joints[8].rotation[1] = float.Parse(f.elements[27]);
            Joints[8].rotation[2] = float.Parse(f.elements[29]);

            Joints[9].rotation[0] = float.Parse(f.elements[31]);
            Joints[9].rotation[1] = float.Parse(f.elements[30]);
            Joints[9].rotation[2] = float.Parse(f.elements[32]);

            Joints[10].rotation[0] = float.Parse(f.elements[34]);
            Joints[10].rotation[1] = float.Parse(f.elements[33]);
            Joints[10].rotation[2] = float.Parse(f.elements[35]);

            Joints[11].rotation[0] = float.Parse(f.elements[37]);
            Joints[11].rotation[1] = float.Parse(f.elements[36]);
            Joints[11].rotation[2] = float.Parse(f.elements[38]);

            Joints[12].rotation[0] = float.Parse(f.elements[40]);
            Joints[12].rotation[1] = float.Parse(f.elements[39]);
            Joints[12].rotation[2] = float.Parse(f.elements[41]);

            Joints[13].rotation[0] = float.Parse(f.elements[43]);
            Joints[13].rotation[1] = float.Parse(f.elements[42]);
            Joints[13].rotation[2] = float.Parse(f.elements[44]);

            Joints[14].rotation[0] = float.Parse(f.elements[46]);
            Joints[14].rotation[1] = float.Parse(f.elements[45]);
            Joints[14].rotation[2] = float.Parse(f.elements[47]);

            Joints[15].rotation[0] = float.Parse(f.elements[49]);
            Joints[15].rotation[1] = float.Parse(f.elements[48]);
            Joints[15].rotation[2] = float.Parse(f.elements[50]);

            Joints[16].rotation[0] = float.Parse(f.elements[52]);
            Joints[16].rotation[1] = float.Parse(f.elements[51]);
            Joints[16].rotation[2] = float.Parse(f.elements[53]);

            Joints[17].rotation[0] = float.Parse(f.elements[55]);
            Joints[17].rotation[1] = float.Parse(f.elements[54]);
            Joints[17].rotation[2] = float.Parse(f.elements[56]);

            Joints[18].rotation[0] = float.Parse(f.elements[58]);
            Joints[18].rotation[1] = float.Parse(f.elements[57]);
            Joints[18].rotation[2] = float.Parse(f.elements[59]);

            //这里的赋值 跟模型的建造紧密相关，注意父子顺序

            //此处计算motion矩阵

            Vector4 quat = new Vector4(0f, 0f, 0f, 1f);

            for (int i = 0; i < numJoints; i++)
            {
                Vector3 tmp = new Vector3(Joints[i].rotation[0], Joints[i].rotation[1], Joints[i].rotation[2]);
                quat = AngleQuaternion(tmp);

                MilkshapeMatrix3x4 matAnimate = new MilkshapeMatrix3x4();
                QuaternionMatrix(quat, matAnimate);
                matAnimate.v[0][3] = Joints[i].position[0];
                matAnimate.v[1][3] = Joints[i].position[1];
                matAnimate.v[2][3] = Joints[i].position[2];

                // animate the local joint matrix using: matLocal = matLocalSkeleton * matAnimate
                if (Joints[i].matLocal == null)
                    Joints[i].matLocal = new MilkshapeMatrix3x4();
                R_ConcatTransforms(Joints[i].matLocalSkeleton, matAnimate, Joints[i].matLocal);

                // build up the hierarchy if joints
                if (Joints[i].parentIndex == -1)
                {
                    if (Joints[i].matGlobal == null)
                        Joints[i].matGlobal = new MilkshapeMatrix3x4();
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 4; l++)
                            Joints[i].matGlobal.v[k][l] = Joints[i].matLocal.v[k][l];
                }
                else
                {
                    MilkshapeJoint parentJoint = Joints[Joints[i].parentIndex];
                    if (Joints[i].matGlobal == null)
                        Joints[i].matGlobal = new MilkshapeMatrix3x4();
                    if (parentJoint.matGlobal == null)
                        parentJoint.matGlobal = new MilkshapeMatrix3x4();
                    R_ConcatTransforms(parentJoint.matGlobal, Joints[i].matLocal, Joints[i].matGlobal);
                }
            }

            //for (int i = 0; i < m_Joint.Count; i++)
            //{
            //    Vector4fEX quat = new Vector4fEX(0.0f, 0.0f, 0.0f, 1.0f);
            //    Vector3fEX tmp = new Vector3fEX(m_Joint[i].rotation.x, m_Joint[i].rotation.y, m_Joint[i].rotation.z);
            //    quat = AngleQuaternion(tmp);

            //    Vector3fEX pos = new Vector3fEX(m_Joint[i].location.x, m_Joint[i].location.y, m_Joint[i].location.z);

            //    MilkshapeMatrix3x4 matAnimate = new MilkshapeMatrix3x4();
            //    QuaternionMatrix(quat, matAnimate);
            //    matAnimate.v[0][3] = pos.x;
            //    matAnimate.v[1][3] = pos.y;
            //    matAnimate.v[2][3] = pos.z;


            //    // animate the local joint matrix using: matLocal = matLocalSkeleton * matAnimate
            //    if (m_Joint[i].m_CurLocalMartic == null)
            //        m_Joint[i].m_CurLocalMartic = new MilkshapeMatrix3x4();
            //    R_ConcatTransforms(m_Joint[i].m_StaLocalMartic, matAnimate, m_Joint[i].m_CurLocalMartic);
            //}


            ////计算当前的两个矩阵，
            //for (int i = 0; i < m_Joint.Count; i++)
            //{
            //    if (m_Joint[i].ParentId == -1)
            //    {
            //        if (m_Joint[i].m_CurGlobMartic == null)
            //            m_Joint[i].m_CurGlobMartic = new MilkshapeMatrix3x4();
            //        for (int k = 0; k < 3; k++)
            //            for (int l = 0; l < 4; l++)
            //                m_Joint[i].m_CurGlobMartic.v[k][l] = m_Joint[i].m_CurLocalMartic.v[k][l];//have question
            //    }
            //    else
            //    {
            //        if (m_Joint[i].m_CurGlobMartic == null)
            //            m_Joint[i].m_CurGlobMartic = new MilkshapeMatrix3x4();
            //        if (m_Joint[m_Joint[i].ParentId].m_CurGlobMartic == null)
            //            m_Joint[m_Joint[i].ParentId].m_CurGlobMartic = new MilkshapeMatrix3x4();
            //        R_ConcatTransforms(m_Joint[m_Joint[i].ParentId].m_CurGlobMartic, m_Joint[i].m_CurLocalMartic, m_Joint[i].m_CurGlobMartic);
            //    }
            //}

            ////更新所有绑定的点
            //UpdateVertex();

            //Render();
        }

        private void FillJointIndicesAndWeights(CustomVertexStruct vertex, MilkshapeJointAndWeight jw)
        {
            jw.jointIndices[0] = vertex.boneID;
            jw.jointIndices[1] = (int)vertex.boneIDs.X;
            jw.jointIndices[2] = (int)vertex.boneIDs.Y;
            jw.jointIndices[3] = (int)vertex.boneIDs.Z;
            jw.jointWeights[0] = 100;
            jw.jointWeights[1] = 0;
            jw.jointWeights[2] = 0;
            jw.jointWeights[3] = 0;
            if (vertex.boneWeights.X != 0 || vertex.boneWeights.Y != 0 || vertex.boneWeights.Z != 0)
            {
                jw.jointWeights[0] = (int)vertex.boneWeights.X;
                jw.jointWeights[1] = (int)vertex.boneWeights.Y;
                jw.jointWeights[2] = (int)vertex.boneWeights.Z;
                jw.jointWeights[3] = 100 - (int)(vertex.boneWeights.X + vertex.boneWeights.Y + vertex.boneWeights.Z);
            }
        }
        
        Vector3 VectorRotate(Vector3 in1, MilkshapeMatrix3x4 in2)
        {
            Vector3 outVert = new Vector3();
            outVert.X = (in1.X * in2.v[0][0] + in1.Y * in2.v[0][1] + in1.Z * in2.v[0][2]);
            outVert.Y = (in1.X * in2.v[1][0] + in1.Y * in2.v[1][1] + in1.Z * in2.v[1][2]);
            outVert.Z = (in1.X * in2.v[2][0] + in1.Y * in2.v[2][1] + in1.Z * in2.v[2][2]);
            return outVert;
        }

        private Vector3 VectorIRotate(Vector3 in1, MilkshapeMatrix3x4 in2)
        {
            Vector3 outVert = new Vector3();
            outVert.X = in1.X * in2.v[0][0] + in1.Y * in2.v[1][0] + in1.Z * in2.v[2][0];
            outVert.Y = in1.X * in2.v[0][1] + in1.Y * in2.v[1][1] + in1.Z * in2.v[2][1];
            outVert.Z = in1.X * in2.v[0][2] + in1.Y * in2.v[1][2] + in1.Z * in2.v[2][2];
            return outVert;
        }

        private Vector3 VectorTransform(Vector3 in1, MilkshapeMatrix3x4 in2)
        {
            Vector3 outVert = new Vector3();
            outVert.X = (in1.X * in2.v[0][0] + in1.Y * in2.v[0][1] + in1.Z * in2.v[0][2]) + in2.v[0][3];
            outVert.Y = (in1.X * in2.v[1][0] + in1.Y * in2.v[1][1] + in1.Z * in2.v[1][2]) + in2.v[1][3];
            outVert.Z = (in1.X * in2.v[2][0] + in1.Y * in2.v[2][1] + in1.Z * in2.v[2][2]) + in2.v[2][3];

            return outVert;
        }

        private Vector3 VectorITransform(Vector3 in1, MilkshapeMatrix3x4 in2)
        {
            Vector3 outVect = new Vector3();
            Vector3 tmp = new Vector3();
            tmp.X = in1.X - in2.v[0][3];
            tmp.Y = in1.Y - in2.v[1][3];
            tmp.Z = in1.Z - in2.v[2][3];
            outVect = VectorIRotate(tmp, in2);
            return outVect;
        }
        
        private CustomVertexStruct TransformVertex(CustomVertexStruct vertex)
        {
            //trebuie optimizata rau de tot
            MilkshapeJointAndWeight jw = new MilkshapeJointAndWeight();
            CustomVertexStruct outVertex = vertex;
            jw.jointIndices[0] = (int)outVertex.boneIDs.X;
            jw.jointIndices[1] = (int)outVertex.boneIDs.Y;
            jw.jointIndices[2] = (int)outVertex.boneIDs.Z;
            jw.jointIndices[3] = (int)outVertex.boneIDs.W;
            jw.jointWeights[0] = (int)(outVertex.boneWeights.X * 100);
            jw.jointWeights[1] = (int)(outVertex.boneWeights.Y * 100);
            jw.jointWeights[2] = (int)(outVertex.boneWeights.Z * 100);
            jw.jointWeights[3] = (int)(outVertex.boneWeights.W * 100);

            if (jw.jointIndices[0] < 0 || jw.jointIndices[0] >= numJoints || currentTime < 0.0f)
            {
            }
            else
            {
                // count valid weights
                int numWeights = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (jw.jointWeights[i] > 0 && jw.jointIndices[i] >= 0 && jw.jointIndices[i] < numJoints)
                        ++numWeights;
                    else
                        break;
                }

                // init
                outVertex.Position.X = 0.0f;
                outVertex.Position.Y = 0.0f;
                outVertex.Position.Z = 0.0f;
                outVertex.Normal.X = 0.0f;
                outVertex.Normal.Y = 0.0f;
                outVertex.Normal.Z = 0.0f;

                //float weights[4] = { (float) jointWeights[0] / 100.0f, (float) jointWeights[1] / 100.0f, (float) jointWeights[2] / 100.0f, (float) jointWeights[3] / 100.0f };
                float[] weights = new float[4];
                weights[0] = (float)jw.jointWeights[0] / 100.0f;
                weights[1] = (float)jw.jointWeights[1] / 100.0f;
                weights[2] = (float)jw.jointWeights[2] / 100.0f;
                weights[3] = (float)jw.jointWeights[3] / 100.0f;

                if (numWeights == 0)
                {
                    numWeights = 1;
                    weights[0] = 1.0f;
                }
                // add weighted vertices
                for (int i = 0; i < numWeights; i++)
                {
                    MilkshapeJoint joint = Joints[jw.jointIndices[i]];
                    Vector3 tmp, vert, norm;
                    tmp = VectorITransform(vertex.Position, joint.matGlobalSkeleton);
                    vert = VectorTransform(tmp, joint.matGlobal);

                    outVertex.Position.X += vert.X * weights[i];
                    outVertex.Position.Y += vert.Y * weights[i];
                    outVertex.Position.Z += vert.Z * weights[i];

                    tmp = new Vector3(0f, 0f, 0f);
                    tmp = VectorIRotate(vertex.Normal, joint.matGlobalSkeleton);
                    //norm = VectorTransform(tmp, joint.matGlobal);

                    norm = VectorRotate(tmp, joint.matGlobal);
                    outVertex.Normal.X += norm.X;
                    outVertex.Normal.Y += norm.Y;
                    outVertex.Normal.Z += norm.Z;
                }
            }
            return outVertex;
        }

        public void rebuildJoints()
        {
            if (currentTime < 0.0f)
            {
                for (int i = 0; i < numJoints; i++)
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 4; l++)
                        {
                            Joints[i].matGlobal.v[k][l] = Joints[i].matGlobalSkeleton.v[k][l];
                            Joints[i].matLocalSkeleton.v[k][l] = Joints[i].matLocal.v[k][l];
                        }
            }
            else
                for (int i = 0; i < numJoints; i++)
                {
                    rebuildJoint(Joints[i]);
                }
        }

        //private bool FirstRun;


        //现在只解析旋转角的值，位移的值 暂时不解析他
        public void advanceAnimation(string data)
        {
    
            Tokens f = new Tokens(data, new char[] { ' ', '\t', '|' });

            //numJoints  关节点个数
            //pos
            //Joints[0].position[0] = float.Parse(f.elements[0]); //f.elements 值得顺序貌似是xyz...接收打数据是zxy rot。
            //Joints[0].position[1] = float.Parse(f.elements[1]);
            //Joints[0].position[2] = float.Parse(f.elements[2]);

            Joints[0].rotation[0] = float.Parse(f.elements[4]) * (float)System.Math.PI / 180;
            Joints[0].rotation[1] = float.Parse(f.elements[5]) * (float)System.Math.PI / 180;
            Joints[0].rotation[2] = float.Parse(f.elements[3]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[0].rotation[0], Joints[0].rotation[1], Joints[0].rotation[2]), 0); //hip

            Joints[1].rotation[0] = float.Parse(f.elements[16]) * (float)System.Math.PI / 180;
            Joints[1].rotation[1] = (float.Parse(f.elements[17])) * (float)System.Math.PI / 180;
            Joints[1].rotation[2] = (float.Parse(f.elements[15])) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[1].rotation[0], Joints[1].rotation[1], Joints[1].rotation[2]), 1); //left up leg

            Joints[3].rotation[0] = float.Parse(f.elements[19]) * (float)System.Math.PI / 180;
            Joints[3].rotation[1] = float.Parse(f.elements[20]) * (float)System.Math.PI / 180;
            Joints[3].rotation[2] = float.Parse(f.elements[18]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[3].rotation[0], Joints[3].rotation[1], Joints[3].rotation[2]), 3); //left down leg

            Joints[5].rotation[0] = float.Parse(f.elements[22]) * (float)System.Math.PI / 180;
            Joints[5].rotation[1] = float.Parse(f.elements[23]) * (float)System.Math.PI / 180;
            Joints[5].rotation[2] = float.Parse(f.elements[21]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[5].rotation[0], Joints[5].rotation[1], Joints[5].rotation[2]), 5); //left foot

            Joints[2].rotation[0] = float.Parse(f.elements[7]) * (float)System.Math.PI / 180;
            Joints[2].rotation[1] = float.Parse(f.elements[8]) * (float)System.Math.PI / 180;
            Joints[2].rotation[2] = float.Parse(f.elements[6]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[2].rotation[0], Joints[2].rotation[1], Joints[2].rotation[2]), 2); //right up leg

            Joints[4].rotation[0] = float.Parse(f.elements[10]) * (float)System.Math.PI / 180;
            Joints[4].rotation[1] = float.Parse(f.elements[11]) * (float)System.Math.PI / 180;
            Joints[4].rotation[2] = float.Parse(f.elements[9]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[4].rotation[0], Joints[4].rotation[1], Joints[4].rotation[2]), 4); //right down leg

            Joints[6].rotation[0] = float.Parse(f.elements[13]) * (float)System.Math.PI / 180;
            Joints[6].rotation[1] = float.Parse(f.elements[14]) * (float)System.Math.PI / 180;
            Joints[6].rotation[2] = float.Parse(f.elements[12]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[6].rotation[0], Joints[6].rotation[1], Joints[6].rotation[2]), 6); //right foot

            

            Joints[9].rotation[0] = float.Parse(f.elements[34]) * (float)System.Math.PI / 180;
            Joints[9].rotation[1] = float.Parse(f.elements[35]) * (float)System.Math.PI / 180;
            Joints[9].rotation[2] = float.Parse(f.elements[33]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[9].rotation[0], Joints[9].rotation[1], Joints[9].rotation[2]), 9); //chest

            Joints[10].rotation[0] = float.Parse(f.elements[55]) * (float)System.Math.PI / 180;
            Joints[10].rotation[1] = float.Parse(f.elements[56]) * (float)System.Math.PI / 180;
            Joints[10].rotation[2] = float.Parse(f.elements[54]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[10].rotation[0], Joints[10].rotation[1], Joints[10].rotation[2]), 10); //left shoulder

            Joints[11].rotation[0] = float.Parse(f.elements[58]) * (float)System.Math.PI / 180;
            Joints[11].rotation[1] = float.Parse(f.elements[59]) * (float)System.Math.PI / 180;
            Joints[11].rotation[2] = float.Parse(f.elements[57]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[11].rotation[0], Joints[11].rotation[1], Joints[11].rotation[2]), 11); //left up arm

            Joints[12].rotation[0] = float.Parse(f.elements[61]) * (float)System.Math.PI / 180;
            Joints[12].rotation[1] = float.Parse(f.elements[62]) * (float)System.Math.PI / 180;
            Joints[12].rotation[2] = float.Parse(f.elements[60]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[12].rotation[0], Joints[12].rotation[1], Joints[12].rotation[2]), 12); //left front arm

            Joints[17].rotation[0] = float.Parse(f.elements[64]) * (float)System.Math.PI / 180;
            Joints[17].rotation[1] = float.Parse(f.elements[65]) * (float)System.Math.PI / 180;
            Joints[17].rotation[2] = float.Parse(f.elements[63]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[17].rotation[0], Joints[17].rotation[1], Joints[17].rotation[2]), 17); //left hand


            Joints[13].rotation[0] = float.Parse(f.elements[43]) * (float)System.Math.PI / 180;
            Joints[13].rotation[1] = float.Parse(f.elements[44]) * (float)System.Math.PI / 180;
            Joints[13].rotation[2] = float.Parse(f.elements[42]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[13].rotation[0], Joints[13].rotation[1], Joints[13].rotation[2]), 13); //right shoulder

            Joints[14].rotation[0] = float.Parse(f.elements[46]) * (float)System.Math.PI / 180;
            Joints[14].rotation[1] = float.Parse(f.elements[47]) * (float)System.Math.PI / 180;
            Joints[14].rotation[2] = float.Parse(f.elements[45]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[14].rotation[0], Joints[14].rotation[1], Joints[14].rotation[2]), 14); //right up arm

            Joints[15].rotation[0] = float.Parse(f.elements[49]) * (float)System.Math.PI / 180;
            Joints[15].rotation[1] = float.Parse(f.elements[50]) * (float)System.Math.PI / 180;
            Joints[15].rotation[2] = float.Parse(f.elements[48]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[15].rotation[0], Joints[15].rotation[1], Joints[15].rotation[2]), 15); //right front arm


            Joints[18].rotation[0] = float.Parse(f.elements[52]) * (float)System.Math.PI / 180;
            Joints[18].rotation[1] = float.Parse(f.elements[53]) * (float)System.Math.PI / 180;
            Joints[18].rotation[2] = float.Parse(f.elements[51]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[18].rotation[0], Joints[18].rotation[1], Joints[18].rotation[2]), 18); //right hand

            Joints[16].rotation[0] = float.Parse(f.elements[41]) * (float)System.Math.PI / 180;
            Joints[16].rotation[1] = float.Parse(f.elements[40]) * (float)System.Math.PI / 180;
            Joints[16].rotation[2] = float.Parse(f.elements[39]) * (float)System.Math.PI / 180;
            updatevertex(new Vector3(Joints[16].rotation[0], Joints[16].rotation[1], Joints[16].rotation[2]), 16); //head


            //updatevertex(new Vector3(3.1415926f / 2, 3.1415926f / 2, 0), 0);  //right arm

        }

        //bool UseHardwareSkinning;

        public MilkshapeModel(string FileName)
        {
            loadMS3DFromFile(FileName);
        }

    }
}
