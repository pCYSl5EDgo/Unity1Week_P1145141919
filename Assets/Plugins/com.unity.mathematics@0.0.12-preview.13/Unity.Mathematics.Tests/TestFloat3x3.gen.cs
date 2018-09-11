// GENERATED CODE
using NUnit.Framework;
using static Unity.Mathematics.math;
namespace Unity.Mathematics.Tests
{
    [TestFixture]
    public class TestFloat3x3
    {
        [Test]
        public void float3x3_operator_equal_wide_wide()
        {
            float3x3 a0 = float3x3(492.1576f, -495.206329f, 227.457642f, -147.374054f, -222.682f, 64.09375f, -23.8904114f, -16.8197327f, 163.232117f);
            float3x3 b0 = float3x3(192.568787f, -235.611023f, -254.043121f, -412.624725f, 471.9048f, -6.47277832f, -339.102356f, 488.187561f, -379.5966f);
            bool3x3 r0 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a0 == b0, r0);

            float3x3 a1 = float3x3(-165.271f, 470.8777f, -423.942566f, 109.6344f, 462.6903f, -335.38147f, 357.2345f, 1.54559326f, -347.388245f);
            float3x3 b1 = float3x3(-308.417f, -82.333374f, -102.921082f, 226.515747f, -356.9013f, -362.912781f, -427.898438f, 466.650146f, -102.799042f);
            bool3x3 r1 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a1 == b1, r1);

            float3x3 a2 = float3x3(-114.472168f, 435.848633f, 194.2381f, 138.765564f, -467.349152f, 370.43335f, 476.708252f, 320.552673f, -498.59198f);
            float3x3 b2 = float3x3(-43.355957f, 85.0456543f, -91.1270447f, 422.192078f, -477.4313f, 1.87701416f, 312.580078f, 254.599365f, 352.72583f);
            bool3x3 r2 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a2 == b2, r2);

            float3x3 a3 = float3x3(92.41693f, 104.511353f, 166.754578f, -204.733429f, 434.756775f, -397.329651f, 503.981628f, -503.7141f, 90.65973f);
            float3x3 b3 = float3x3(62.4909668f, 119.714783f, -511.058075f, -302.472717f, -371.769226f, -20.007843f, 21.4594727f, -426.0207f, -305.411926f);
            bool3x3 r3 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a3 == b3, r3);
        }

        [Test]
        public void float3x3_operator_equal_wide_scalar()
        {
            float3x3 a0 = float3x3(-303.230072f, 451.5263f, -253.655884f, -105.203644f, -500.6911f, -426.192474f, 159.8761f, -59.55838f, -57.4773865f);
            float b0 = (123.544556f);
            bool3x3 r0 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a0 == b0, r0);

            float3x3 a1 = float3x3(-182.049744f, 370.886f, -172.035309f, 455.400024f, -11.3389893f, 363.938232f, -27.1505737f, -325.976074f, -290.359039f);
            float b1 = (406.513733f);
            bool3x3 r1 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a1 == b1, r1);

            float3x3 a2 = float3x3(180.196838f, -439.358948f, -126.546082f, -197.26178f, -227.159332f, -479.8992f, -439.777679f, -495.237335f, -224.517059f);
            float b2 = (-374.128326f);
            bool3x3 r2 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a2 == b2, r2);

            float3x3 a3 = float3x3(-422.833221f, -20.10672f, 297.38f, 185.966553f, -102.975983f, -220.597046f, -228.686859f, -333.001282f, 434.213f);
            float b3 = (-450.196259f);
            bool3x3 r3 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a3 == b3, r3);
        }

        [Test]
        public void float3x3_operator_equal_scalar_wide()
        {
            float a0 = (-253.397278f);
            float3x3 b0 = float3x3(19.95221f, -185.791992f, 407.8136f, -87.2767f, -206.274689f, 160.503113f, -274.7708f, -2.63153076f, 448.354553f);
            bool3x3 r0 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a0 == b0, r0);

            float a1 = (-410.035248f);
            float3x3 b1 = float3x3(247.329041f, 355.539124f, -298.0667f, 414.1015f, -481.3026f, 196.55072f, 34.6010132f, 113.7616f, -386.453369f);
            bool3x3 r1 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a1 == b1, r1);

            float a2 = (-124.49176f);
            float3x3 b2 = float3x3(243.886658f, -492.6182f, 145.424438f, 421.55072f, -95.40997f, 336.809265f, 209.5838f, 487.4414f, 161.806519f);
            bool3x3 r2 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a2 == b2, r2);

            float a3 = (149.842468f);
            float3x3 b3 = float3x3(225.724f, -71.21881f, 85.78027f, 192.547241f, -49.88748f, -229.801971f, -103.407349f, 19.21576f, 492.8811f);
            bool3x3 r3 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a3 == b3, r3);
        }

        [Test]
        public void float3x3_operator_not_equal_wide_wide()
        {
            float3x3 a0 = float3x3(430.842529f, 104.69f, 225.802429f, -310.5702f, -418.619446f, 304.128174f, -509.3268f, -160.538086f, -203.301971f);
            float3x3 b0 = float3x3(210.024719f, -55.20334f, -269.925354f, -234.546722f, 25.91742f, -63.72699f, -484.5537f, -425.3336f, -53.2743835f);
            bool3x3 r0 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a0 != b0, r0);

            float3x3 a1 = float3x3(-505.763245f, 162.17218f, 1.156189f, 65.66205f, 102.787781f, 172.930054f, 26.6210327f, 235.125977f, 128.541992f);
            float3x3 b1 = float3x3(328.1944f, 15.9631348f, 461.7141f, -113.363037f, -240.072968f, 495.119141f, 203.55835f, 340.493469f, -241.9072f);
            bool3x3 r1 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a1 != b1, r1);

            float3x3 a2 = float3x3(-354.996979f, 334.3595f, -495.832f, 468.307373f, 458.370972f, 299.937317f, 43.1271973f, -354.7135f, -145.2872f);
            float3x3 b2 = float3x3(459.569824f, 213.07373f, -384.782837f, -255.072327f, 477.663452f, -248.036621f, -407.923462f, -199.788879f, 151.843262f);
            bool3x3 r2 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a2 != b2, r2);

            float3x3 a3 = float3x3(390.80188f, -303.13147f, 391.134583f, 139.286865f, 104.523193f, 511.2964f, 213.147034f, -101.0957f, 441.6634f);
            float3x3 b3 = float3x3(-97.1206055f, 154.975891f, -172.834534f, 441.5028f, -401.738617f, -411.430176f, -337.820282f, -430.6309f, -150.8718f);
            bool3x3 r3 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a3 != b3, r3);
        }

        [Test]
        public void float3x3_operator_not_equal_wide_scalar()
        {
            float3x3 a0 = float3x3(-16.9145813f, 168.8341f, -462.713531f, 130.307739f, 214.501587f, -440.263275f, -197.12796f, -169.099854f, -386.611176f);
            float b0 = (-145.372772f);
            bool3x3 r0 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a0 != b0, r0);

            float3x3 a1 = float3x3(-281.021f, -403.9637f, -269.805725f, 299.654236f, -71.7509155f, -432.755737f, -457.363129f, -13.5195923f, 273.873047f);
            float b1 = (-270.26886f);
            bool3x3 r1 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a1 != b1, r1);

            float3x3 a2 = float3x3(185.04248f, 116.395142f, 511.735f, 230.5075f, 100.27478f, 129.682434f, 321.178772f, -220.639f, 140.3352f);
            float b2 = (-482.5307f);
            bool3x3 r2 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a2 != b2, r2);

            float3x3 a3 = float3x3(369.212341f, -333.66626f, -373.937744f, 150.204285f, -442.164764f, 372.32f, -95.83798f, 495.5667f, -5.3855896f);
            float b3 = (453.811218f);
            bool3x3 r3 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a3 != b3, r3);
        }

        [Test]
        public void float3x3_operator_not_equal_scalar_wide()
        {
            float a0 = (275.795837f);
            float3x3 b0 = float3x3(-57.1969f, -382.432526f, 97.82037f, -161.463654f, -458.39563f, -499.617859f, 327.92218f, 367.571228f, 59.786377f);
            bool3x3 r0 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a0 != b0, r0);

            float a1 = (-209.580688f);
            float3x3 b1 = float3x3(-62.5804443f, -479.974976f, -49.4945068f, -114.685211f, 109.93927f, -176.284821f, -347.4853f, 85.5409546f, -356.659546f);
            bool3x3 r1 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a1 != b1, r1);

            float a2 = (-104.243561f);
            float3x3 b2 = float3x3(-133.5492f, 243.539734f, 13.1412964f, -379.985962f, -41.28122f, 87.91168f, -339.077271f, -371.820343f, 333.1443f);
            bool3x3 r2 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a2 != b2, r2);

            float a3 = (294.811951f);
            float3x3 b3 = float3x3(-187.14566f, 220.192261f, -228.182068f, -499.723724f, 97.40588f, 501.60437f, 459.6754f, 158.098145f, 358.4886f);
            bool3x3 r3 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a3 != b3, r3);
        }

        [Test]
        public void float3x3_operator_less_wide_wide()
        {
            float3x3 a0 = float3x3(196.84259f, 336.4098f, 251.963745f, 257.655945f, 430.0459f, -62.4196472f, 8.839233f, -333.8167f, 164.678833f);
            float3x3 b0 = float3x3(-465.345032f, -256.1524f, -314.814026f, 364.5667f, 100.21051f, 182.560974f, 3.11700439f, -259.430481f, -437.3349f);
            bool3x3 r0 = bool3x3(false, false, false, true, false, true, false, true, false);
            TestUtils.AreEqual(a0 < b0, r0);

            float3x3 a1 = float3x3(-350.9449f, 3.84143066f, 125.409729f, -111.129944f, 70.00549f, 448.1983f, -419.987122f, -258.301666f, -34.8322144f);
            float3x3 b1 = float3x3(-456.043732f, -394.255981f, 401.9137f, 313.439148f, 121.286682f, -28.0122986f, -282.965881f, 330.0644f, 124.099365f);
            bool3x3 r1 = bool3x3(false, false, true, true, true, false, true, true, true);
            TestUtils.AreEqual(a1 < b1, r1);

            float3x3 a2 = float3x3(-69.8594055f, 67.76721f, -139.777283f, 385.434631f, 133.7074f, 506.188354f, 34.44287f, 412.1137f, -84.8097839f);
            float3x3 b2 = float3x3(-183.6903f, 373.0608f, 109.750916f, -203.57135f, 45.64868f, -360.952271f, 211.913086f, -313.286377f, -259.661072f);
            bool3x3 r2 = bool3x3(false, true, true, false, false, false, true, false, false);
            TestUtils.AreEqual(a2 < b2, r2);

            float3x3 a3 = float3x3(444.785339f, -78.75473f, 366.977539f, 127.180481f, 428.368469f, 8.197632f, -71.13736f, -474.0508f, 322.4289f);
            float3x3 b3 = float3x3(79.09851f, 446.4961f, 450.524536f, -375.630768f, -53.9418335f, -291.453735f, 190.774841f, 54.0839233f, -163.63089f);
            bool3x3 r3 = bool3x3(false, true, true, false, false, false, true, true, false);
            TestUtils.AreEqual(a3 < b3, r3);
        }

        [Test]
        public void float3x3_operator_less_wide_scalar()
        {
            float3x3 a0 = float3x3(-132.057312f, -192.465f, -66.8345947f, -379.017517f, -360.2824f, 20.9278564f, -158.240753f, 437.3459f, -20.4526062f);
            float b0 = (-156.010223f);
            bool3x3 r0 = bool3x3(false, true, false, true, true, false, true, false, false);
            TestUtils.AreEqual(a0 < b0, r0);

            float3x3 a1 = float3x3(225.2915f, 274.015259f, 373.549683f, 398.523682f, 105.030151f, -58.0108948f, 109.670105f, -108.85318f, -44.9712524f);
            float b1 = (307.4842f);
            bool3x3 r1 = bool3x3(true, true, false, false, true, true, true, true, true);
            TestUtils.AreEqual(a1 < b1, r1);

            float3x3 a2 = float3x3(140.426086f, 172.103333f, -197.500732f, -7.271515f, -432.9905f, 62.1583252f, -72.25473f, -377.852325f, -500.255737f);
            float b2 = (-500.0883f);
            bool3x3 r2 = bool3x3(false, false, false, false, false, false, false, false, true);
            TestUtils.AreEqual(a2 < b2, r2);

            float3x3 a3 = float3x3(149.11499f, 202.63916f, 274.950684f, 66.41205f, 274.999451f, -149.6358f, 223.758728f, 73.2668457f, 213.094971f);
            float b3 = (119.880615f);
            bool3x3 r3 = bool3x3(false, false, false, true, false, true, false, true, false);
            TestUtils.AreEqual(a3 < b3, r3);
        }

        [Test]
        public void float3x3_operator_less_scalar_wide()
        {
            float a0 = (-423.1174f);
            float3x3 b0 = float3x3(385.094849f, -123.933472f, 86.37659f, 133.4422f, 161.457947f, 229.754272f, 222.5716f, 315.5312f, -447.203522f);
            bool3x3 r0 = bool3x3(true, true, true, true, true, true, true, true, false);
            TestUtils.AreEqual(a0 < b0, r0);

            float a1 = (271.833862f);
            float3x3 b1 = float3x3(-393.605316f, 317.486877f, -164.6051f, -282.876038f, 296.979553f, -254.401154f, 365.6156f, -441.984253f, -131.42865f);
            bool3x3 r1 = bool3x3(false, true, false, false, true, false, true, false, false);
            TestUtils.AreEqual(a1 < b1, r1);

            float a2 = (442.628967f);
            float3x3 b2 = float3x3(-29.7928467f, -138.37381f, 9.21698f, -226.73056f, 171.029419f, 376.625244f, -462.5887f, -142.36731f, -456.253784f);
            bool3x3 r2 = bool3x3(false, false, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a2 < b2, r2);

            float a3 = (66.61023f);
            float3x3 b3 = float3x3(169.378784f, 327.4444f, 64.08795f, -153.5039f, 199.380127f, -244.969055f, 472.743835f, -363.7801f, -179.487518f);
            bool3x3 r3 = bool3x3(true, true, false, false, true, false, true, false, false);
            TestUtils.AreEqual(a3 < b3, r3);
        }

        [Test]
        public void float3x3_operator_greater_wide_wide()
        {
            float3x3 a0 = float3x3(483.5014f, 310.8156f, 106.966187f, 295.7353f, 116.957581f, -478.299774f, -14.8974f, -33.8174438f, -24.74054f);
            float3x3 b0 = float3x3(-471.398f, -371.9853f, 36.9006958f, -316.7636f, 19.6830444f, 207.309143f, 362.7975f, 324.95343f, 340.948059f);
            bool3x3 r0 = bool3x3(true, true, true, true, true, false, false, false, false);
            TestUtils.AreEqual(a0 > b0, r0);

            float3x3 a1 = float3x3(319.782654f, -120.158569f, -289.008575f, 455.85144f, 144.706909f, 63.9320068f, -285.683044f, -502.090729f, -337.194458f);
            float3x3 b1 = float3x3(25.9860229f, -114.211121f, 240.803467f, 273.422424f, 325.515747f, 27.3410645f, 64.47955f, 200.948364f, 100.122681f);
            bool3x3 r1 = bool3x3(true, false, false, true, false, true, false, false, false);
            TestUtils.AreEqual(a1 > b1, r1);

            float3x3 a2 = float3x3(474.317322f, -507.1451f, -133.565582f, -443.109131f, -464.34137f, -68.36154f, -185.993011f, -157.8039f, -74.12424f);
            float3x3 b2 = float3x3(-79.00711f, -315.137939f, -122.985443f, -163.7792f, -492.566f, -90.79727f, -284.901245f, -23.6536865f, 174.93f);
            bool3x3 r2 = bool3x3(true, false, false, false, true, true, true, false, false);
            TestUtils.AreEqual(a2 > b2, r2);

            float3x3 a3 = float3x3(-94.47116f, 329.610535f, -315.836731f, 404.1938f, 131.304382f, -206.633911f, 197.399841f, 187.991943f, 362.636047f);
            float3x3 b3 = float3x3(85.7125244f, -441.987823f, 345.543762f, 482.219482f, -422.383484f, -30.7792969f, 296.154236f, 378.059875f, -457.733429f);
            bool3x3 r3 = bool3x3(false, true, false, false, true, false, false, false, true);
            TestUtils.AreEqual(a3 > b3, r3);
        }

        [Test]
        public void float3x3_operator_greater_wide_scalar()
        {
            float3x3 a0 = float3x3(64.31793f, -397.703461f, 431.8769f, 85.703f, 246.263062f, 197.491577f, 286.199463f, 280.813354f, -405.7846f);
            float b0 = (305.859924f);
            bool3x3 r0 = bool3x3(false, false, true, false, false, false, false, false, false);
            TestUtils.AreEqual(a0 > b0, r0);

            float3x3 a1 = float3x3(171.565369f, 333.5782f, 370.279175f, -413.7014f, -356.592346f, -353.0313f, 396.645325f, 467.222046f, -240.013428f);
            float b1 = (-241.807281f);
            bool3x3 r1 = bool3x3(true, true, true, false, false, false, true, true, true);
            TestUtils.AreEqual(a1 > b1, r1);

            float3x3 a2 = float3x3(502.915039f, -259.2897f, 281.230652f, 428.792175f, 245.153076f, -279.1754f, -453.8631f, -124.771545f, -425.652924f);
            float b2 = (315.4676f);
            bool3x3 r2 = bool3x3(true, false, false, true, false, false, false, false, false);
            TestUtils.AreEqual(a2 > b2, r2);

            float3x3 a3 = float3x3(99.13287f, -456.439423f, 154.489014f, 405.529724f, -157.7338f, 186.0863f, 249.999084f, -110.096924f, -435.3045f);
            float b3 = (355.060547f);
            bool3x3 r3 = bool3x3(false, false, false, true, false, false, false, false, false);
            TestUtils.AreEqual(a3 > b3, r3);
        }

        [Test]
        public void float3x3_operator_greater_scalar_wide()
        {
            float a0 = (-282.6705f);
            float3x3 b0 = float3x3(358.099976f, -72.596405f, -232.163788f, -60.7067261f, 75.15662f, 150.883484f, 339.539185f, -498.196045f, 459.7461f);
            bool3x3 r0 = bool3x3(false, false, false, false, false, false, false, true, false);
            TestUtils.AreEqual(a0 > b0, r0);

            float a1 = (-227.968719f);
            float3x3 b1 = float3x3(335.862122f, 76.17883f, 296.859924f, 177.48999f, -281.2012f, 244.722839f, 137.328552f, -385.338257f, 443.163452f);
            bool3x3 r1 = bool3x3(false, false, false, false, true, false, false, true, false);
            TestUtils.AreEqual(a1 > b1, r1);

            float a2 = (-353.562561f);
            float3x3 b2 = float3x3(26.04065f, -331.793945f, -43.6919556f, 20.9494019f, -211.17984f, 227.421692f, -84.7797852f, -375.1355f, -205.178131f);
            bool3x3 r2 = bool3x3(false, false, false, false, false, false, false, true, false);
            TestUtils.AreEqual(a2 > b2, r2);

            float a3 = (-197.04715f);
            float3x3 b3 = float3x3(-219.634033f, -210.015625f, -266.7737f, 144.7785f, -471.7112f, -155.913177f, 99.72473f, -230.944855f, -338.8689f);
            bool3x3 r3 = bool3x3(true, true, true, false, true, false, false, true, true);
            TestUtils.AreEqual(a3 > b3, r3);
        }

        [Test]
        public void float3x3_operator_less_equal_wide_wide()
        {
            float3x3 a0 = float3x3(-438.523132f, 210.489441f, 4.87731934f, -137.297943f, 156.094116f, -363.924133f, -97.94849f, 437.2954f, 458.530273f);
            float3x3 b0 = float3x3(-474.814148f, 304.371033f, 234.824158f, -390.485443f, -297.175354f, -326.2924f, 107.253906f, -413.131073f, 67.09442f);
            bool3x3 r0 = bool3x3(false, true, true, false, false, true, true, false, false);
            TestUtils.AreEqual(a0 <= b0, r0);

            float3x3 a1 = float3x3(-294.064758f, 23.62262f, -34.2840576f, 149.736511f, -418.8867f, -197.502533f, -88.2055054f, -376.71814f, 341.627136f);
            float3x3 b1 = float3x3(470.075256f, -84.499115f, 392.784241f, -263.531738f, 369.3009f, -333.3253f, 238.413452f, 486.2426f, 279.6502f);
            bool3x3 r1 = bool3x3(true, false, true, false, true, false, true, true, false);
            TestUtils.AreEqual(a1 <= b1, r1);

            float3x3 a2 = float3x3(-83.30917f, -107.490723f, 319.466858f, 205.357361f, 345.563721f, 395.3219f, -222.874146f, 439.022034f, -368.075562f);
            float3x3 b2 = float3x3(236.052f, 132.758972f, 66.29474f, 183.002136f, 200.130554f, 339.043823f, 438.5379f, 145.401855f, 178.163086f);
            bool3x3 r2 = bool3x3(true, true, false, false, false, false, true, false, true);
            TestUtils.AreEqual(a2 <= b2, r2);

            float3x3 a3 = float3x3(-200.0386f, 71.46991f, -357.365417f, 141.710876f, 319.0171f, 303.030151f, -461.574249f, 277.62677f, 182.1781f);
            float3x3 b3 = float3x3(157.975952f, 329.7052f, -243.590912f, 5.401184f, -22.5805969f, -90.3375854f, -72.19107f, -354.354828f, -289.521729f);
            bool3x3 r3 = bool3x3(true, true, true, false, false, false, true, false, false);
            TestUtils.AreEqual(a3 <= b3, r3);
        }

        [Test]
        public void float3x3_operator_less_equal_wide_scalar()
        {
            float3x3 a0 = float3x3(193.49585f, 168.915527f, -313.993073f, 81.8269653f, 18.5036011f, -0.3581848f, 241.361145f, -463.8164f, -1.35775757f);
            float b0 = (443.850525f);
            bool3x3 r0 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a0 <= b0, r0);

            float3x3 a1 = float3x3(-268.899475f, -471.253082f, -264.9378f, 82.2583f, 11.2460327f, 424.704041f, 426.482239f, 56.3200073f, -196.2879f);
            float b1 = (398.991943f);
            bool3x3 r1 = bool3x3(true, true, true, true, true, false, false, true, true);
            TestUtils.AreEqual(a1 <= b1, r1);

            float3x3 a2 = float3x3(31.9011841f, -437.926453f, -37.1048279f, -47.1442261f, 333.623047f, -274.8039f, 358.67627f, -260.460571f, 192.309143f);
            float b2 = (-152.257568f);
            bool3x3 r2 = bool3x3(false, true, false, false, false, true, false, true, false);
            TestUtils.AreEqual(a2 <= b2, r2);

            float3x3 a3 = float3x3(145.306091f, -494.267334f, -111.570129f, -139.5412f, -146.589355f, 33.98401f, -445.704468f, -451.0422f, -122.039276f);
            float b3 = (-466.132965f);
            bool3x3 r3 = bool3x3(false, true, false, false, false, false, false, false, false);
            TestUtils.AreEqual(a3 <= b3, r3);
        }

        [Test]
        public void float3x3_operator_less_equal_scalar_wide()
        {
            float a0 = (393.606262f);
            float3x3 b0 = float3x3(-75.6883545f, -44.2638855f, 125.864929f, 191.9649f, 13.54303f, -197.051941f, -423.9451f, -330.0486f, 420.165527f);
            bool3x3 r0 = bool3x3(false, false, false, false, false, false, false, false, true);
            TestUtils.AreEqual(a0 <= b0, r0);

            float a1 = (105.5473f);
            float3x3 b1 = float3x3(174.821289f, 296.7176f, -469.7004f, 123.267212f, 112.996948f, 495.143372f, -488.6579f, 388.539429f, -493.240784f);
            bool3x3 r1 = bool3x3(true, true, false, true, true, true, false, true, false);
            TestUtils.AreEqual(a1 <= b1, r1);

            float a2 = (16.45105f);
            float3x3 b2 = float3x3(-387.651642f, -229.1773f, -373.01532f, -391.142151f, 90.99414f, -178.396149f, -69.62106f, 471.790833f, -67.46677f);
            bool3x3 r2 = bool3x3(false, false, false, false, true, false, false, true, false);
            TestUtils.AreEqual(a2 <= b2, r2);

            float a3 = (45.30536f);
            float3x3 b3 = float3x3(-154.6922f, 385.7389f, -431.652954f, -331.673035f, -349.8927f, -114.839142f, -245.217834f, -486.6955f, 391.950928f);
            bool3x3 r3 = bool3x3(false, true, false, false, false, false, false, false, true);
            TestUtils.AreEqual(a3 <= b3, r3);
        }

        [Test]
        public void float3x3_operator_greater_equal_wide_wide()
        {
            float3x3 a0 = float3x3(-507.9286f, 504.4975f, -385.4345f, -262.323425f, -37.5509338f, -111.595276f, -463.702026f, 387.448853f, 456.9688f);
            float3x3 b0 = float3x3(-81.3465f, 297.666138f, 171.06543f, -431.038055f, -6.85907f, 319.7257f, 254.079163f, 396.5724f, 178.8393f);
            bool3x3 r0 = bool3x3(false, true, false, true, false, false, false, false, true);
            TestUtils.AreEqual(a0 >= b0, r0);

            float3x3 a1 = float3x3(-211.010162f, 182.411377f, -53.59604f, -309.570221f, -136.022491f, 280.736267f, -96.99588f, -174.059509f, 88.90192f);
            float3x3 b1 = float3x3(-447.063354f, 288.492676f, 474.889282f, -321.750244f, -395.977234f, -158.692474f, 391.4887f, -368.109253f, 89.12378f);
            bool3x3 r1 = bool3x3(true, false, false, true, true, true, false, true, false);
            TestUtils.AreEqual(a1 >= b1, r1);

            float3x3 a2 = float3x3(43.81604f, -446.07843f, 16.6455688f, 409.83252f, -191.329865f, 222.9978f, 404.2884f, 230.603271f, -441.789276f);
            float3x3 b2 = float3x3(-510.279327f, -486.9298f, -81.2155457f, 274.2188f, -212.881561f, 288.9953f, 307.73175f, 307.245178f, -199.391785f);
            bool3x3 r2 = bool3x3(true, true, true, true, true, false, true, false, false);
            TestUtils.AreEqual(a2 >= b2, r2);

            float3x3 a3 = float3x3(-86.29306f, 484.249573f, 95.23639f, -204.912109f, -199.774353f, -421.8632f, -18.2147827f, -346.822754f, -159.243652f);
            float3x3 b3 = float3x3(-284.421265f, -482.3918f, 448.315735f, -378.3462f, -390.858459f, 8.916016f, 416.407227f, -213.674957f, 455.2481f);
            bool3x3 r3 = bool3x3(true, true, false, true, true, false, false, false, false);
            TestUtils.AreEqual(a3 >= b3, r3);
        }

        [Test]
        public void float3x3_operator_greater_equal_wide_scalar()
        {
            float3x3 a0 = float3x3(465.152161f, -424.886078f, -209.2211f, 58.7798462f, -302.2691f, 140.12561f, 16.3533936f, -344.559967f, 393.278076f);
            float b0 = (-5.599884f);
            bool3x3 r0 = bool3x3(true, false, false, true, false, true, true, false, true);
            TestUtils.AreEqual(a0 >= b0, r0);

            float3x3 a1 = float3x3(-315.701538f, -509.781555f, -36.9942932f, 494.8203f, -164.973938f, -466.1201f, -123.813751f, 215.651245f, 104.995728f);
            float b1 = (441.011536f);
            bool3x3 r1 = bool3x3(false, false, false, true, false, false, false, false, false);
            TestUtils.AreEqual(a1 >= b1, r1);

            float3x3 a2 = float3x3(314.346f, -83.11142f, -23.8364258f, 143.049377f, -264.919983f, -169.702209f, 329.70752f, 359.095825f, -260.4233f);
            float b2 = (190.516113f);
            bool3x3 r2 = bool3x3(true, false, false, false, false, false, true, true, false);
            TestUtils.AreEqual(a2 >= b2, r2);

            float3x3 a3 = float3x3(354.195129f, 33.309082f, 355.6313f, -435.360565f, -38.3993225f, -93.2957153f, -338.8496f, 436.8958f, 511.084167f);
            float b3 = (-111.845337f);
            bool3x3 r3 = bool3x3(true, true, true, false, true, true, false, true, true);
            TestUtils.AreEqual(a3 >= b3, r3);
        }

        [Test]
        public void float3x3_operator_greater_equal_scalar_wide()
        {
            float a0 = (374.827026f);
            float3x3 b0 = float3x3(-1.60977173f, 338.615234f, -116.1814f, -332.157318f, -355.97937f, -468.901428f, 38.579895f, -332.347534f, 2.89013672f);
            bool3x3 r0 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a0 >= b0, r0);

            float a1 = (467.777771f);
            float3x3 b1 = float3x3(121.406372f, -305.023376f, -58.4288025f, -226.519562f, -47.0209961f, 305.302673f, -427.401245f, 92.26367f, -497.178528f);
            bool3x3 r1 = bool3x3(true, true, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a1 >= b1, r1);

            float a2 = (-408.625641f);
            float3x3 b2 = float3x3(-455.2305f, 396.4261f, -469.2949f, -485.754028f, -182.346191f, -291.545349f, 278.740784f, -75.8711243f, 28.9070435f);
            bool3x3 r2 = bool3x3(true, false, true, true, false, false, false, false, false);
            TestUtils.AreEqual(a2 >= b2, r2);

            float a3 = (287.720154f);
            float3x3 b3 = float3x3(420.509766f, 473.626831f, 181.514526f, -369.202881f, 243.749756f, -244.124146f, -242.993347f, -322.115356f, 192.974976f);
            bool3x3 r3 = bool3x3(false, false, true, true, true, true, true, true, true);
            TestUtils.AreEqual(a3 >= b3, r3);
        }

        [Test]
        public void float3x3_operator_add_wide_wide()
        {
            float3x3 a0 = float3x3(506.129028f, -501.779816f, 420.084778f, -186.032074f, -9.312408f, 328.51178f, 424.344055f, 87.79108f, 462.4137f);
            float3x3 b0 = float3x3(-28.7579956f, -337.135132f, -340.676819f, 152.312012f, 423.66748f, 90.3740845f, 376.18866f, 1.76721191f, -120.185852f);
            float3x3 r0 = float3x3(477.371033f, -838.9149f, 79.40796f, -33.7200623f, 414.355072f, 418.885864f, 800.5327f, 89.55829f, 342.227844f);
            TestUtils.AreEqual(a0 + b0, r0);

            float3x3 a1 = float3x3(-46.17871f, 401.170044f, -454.124146f, 69.19568f, -177.957336f, 299.604126f, 340.704834f, 219.916016f, -321.9084f);
            float3x3 b1 = float3x3(-279.629364f, -344.6671f, 242.839172f, 418.593079f, -23.3128052f, -95.0999451f, 147.9281f, 331.0329f, -82.50256f);
            float3x3 r1 = float3x3(-325.808075f, 56.50293f, -211.284973f, 487.788757f, -201.270142f, 204.504181f, 488.632935f, 550.9489f, -404.41095f);
            TestUtils.AreEqual(a1 + b1, r1);

            float3x3 a2 = float3x3(286.355347f, -333.4195f, -118.932159f, 68.60748f, 23.190918f, -205.577881f, 11.5214233f, -340.795074f, -68.93118f);
            float3x3 b2 = float3x3(279.4496f, 342.622742f, -300.358521f, -209.694092f, 446.559448f, -351.9892f, -263.12384f, -252.458557f, 289.825378f);
            float3x3 r2 = float3x3(565.804932f, 9.203247f, -419.29068f, -141.086609f, 469.750366f, -557.5671f, -251.602417f, -593.253662f, 220.8942f);
            TestUtils.AreEqual(a2 + b2, r2);

            float3x3 a3 = float3x3(304.8532f, -86.63385f, 105.669128f, 349.280518f, 364.7079f, -429.0374f, 382.458069f, 186.097046f, 227.411865f);
            float3x3 b3 = float3x3(338.796143f, -232.619019f, -510.50824f, 349.280762f, -426.212463f, -331.416321f, -418.6888f, -341.70636f, -329.0359f);
            float3x3 r3 = float3x3(643.649353f, -319.252869f, -404.8391f, 698.5613f, -61.5045776f, -760.453735f, -36.2307434f, -155.609314f, -101.624023f);
            TestUtils.AreEqual(a3 + b3, r3);
        }

        [Test]
        public void float3x3_operator_add_wide_scalar()
        {
            float3x3 a0 = float3x3(-194.514191f, 338.5484f, 246.971375f, 100.510925f, -45.72467f, -478.1113f, 30.9161377f, 60.37433f, -242.118744f);
            float b0 = (124.121704f);
            float3x3 r0 = float3x3(-70.39249f, 462.6701f, 371.093079f, 224.632629f, 78.39703f, -353.9896f, 155.037842f, 184.496033f, -117.99704f);
            TestUtils.AreEqual(a0 + b0, r0);

            float3x3 a1 = float3x3(82.50134f, -484.6998f, -188.265015f, -213.526733f, -267.7843f, 189.259949f, 198.533569f, 187.536072f, -424.925659f);
            float b1 = (6.79937744f);
            float3x3 r1 = float3x3(89.30072f, -477.900421f, -181.465637f, -206.727356f, -260.984924f, 196.059326f, 205.332947f, 194.335449f, -418.126282f);
            TestUtils.AreEqual(a1 + b1, r1);

            float3x3 a2 = float3x3(302.102356f, 124.021606f, -200.161346f, 31.3782349f, 362.522156f, -423.988861f, 432.41333f, 374.211426f, -465.6995f);
            float b2 = (300.3991f);
            float3x3 r2 = float3x3(602.501465f, 424.420715f, 100.237762f, 331.777344f, 662.921265f, -123.589752f, 732.812439f, 674.610535f, -165.300385f);
            TestUtils.AreEqual(a2 + b2, r2);

            float3x3 a3 = float3x3(-311.04303f, -432.442444f, 235.750671f, -472.637756f, -257.577759f, 186.120728f, -170.298218f, -115.272491f, -101.168823f);
            float b3 = (84.91901f);
            float3x3 r3 = float3x3(-226.124023f, -347.523438f, 320.669678f, -387.71875f, -172.658752f, 271.039734f, -85.37921f, -30.3534851f, -16.2498169f);
            TestUtils.AreEqual(a3 + b3, r3);
        }

        [Test]
        public void float3x3_operator_add_scalar_wide()
        {
            float a0 = (-340.354675f);
            float3x3 b0 = float3x3(511.362244f, -146.216644f, -106.210419f, -363.450256f, 199.0896f, -27.1083984f, 419.849f, 284.955017f, -164.9242f);
            float3x3 r0 = float3x3(171.007568f, -486.57132f, -446.5651f, -703.804932f, -141.265076f, -367.463074f, 79.49432f, -55.39966f, -505.27887f);
            TestUtils.AreEqual(a0 + b0, r0);

            float a1 = (-249.190338f);
            float3x3 b1 = float3x3(150.928162f, 298.1751f, -457.1534f, 424.718079f, -301.857483f, 230.288879f, -423.5876f, -67.06003f, 68.72412f);
            float3x3 r1 = float3x3(-98.26218f, 48.98477f, -706.34375f, 175.52774f, -551.047852f, -18.9014587f, -672.777954f, -316.250366f, -180.466217f);
            TestUtils.AreEqual(a1 + b1, r1);

            float a2 = (-164.02243f);
            float3x3 b2 = float3x3(318.935181f, 7.80456543f, 187.698364f, -3.656952f, -446.083069f, -209.287231f, -38.21289f, -346.257172f, 465.607422f);
            float3x3 r2 = float3x3(154.91275f, -156.217865f, 23.6759338f, -167.679382f, -610.105469f, -373.309662f, -202.235321f, -510.2796f, 301.585f);
            TestUtils.AreEqual(a2 + b2, r2);

            float a3 = (-192.185944f);
            float3x3 b3 = float3x3(278.6938f, 381.978455f, 481.243652f, -97.22815f, -455.513733f, 501.834961f, 358.7066f, 430.699768f, 256.987183f);
            float3x3 r3 = float3x3(86.50784f, 189.792511f, 289.0577f, -289.4141f, -647.6997f, 309.649017f, 166.52066f, 238.513824f, 64.80124f);
            TestUtils.AreEqual(a3 + b3, r3);
        }

        [Test]
        public void float3x3_operator_sub_wide_wide()
        {
            float3x3 a0 = float3x3(160.492249f, 11.223938f, 359.200134f, -498.2283f, -355.253632f, -94.53485f, -410.46405f, -401.384644f, 317.706848f);
            float3x3 b0 = float3x3(115.46875f, -130.9823f, 241.540833f, 9.987061f, 419.895142f, 59.12445f, -402.381653f, -75.37015f, 320.9796f);
            float3x3 r0 = float3x3(45.0235f, 142.206238f, 117.6593f, -508.215363f, -775.1488f, -153.6593f, -8.082397f, -326.0145f, -3.272766f);
            TestUtils.AreEqual(a0 - b0, r0);

            float3x3 a1 = float3x3(447.060425f, -489.074158f, -230.008392f, 24.8754272f, 366.614441f, -107.374146f, -219.008148f, 473.9076f, 259.63623f);
            float3x3 b1 = float3x3(-73.90875f, -31.4447327f, -389.251953f, -375.028839f, 259.182739f, 276.648682f, -453.0692f, -272.576538f, -191.148041f);
            float3x3 r1 = float3x3(520.9692f, -457.629425f, 159.243561f, 399.904266f, 107.4317f, -384.022827f, 234.061066f, 746.484131f, 450.784271f);
            TestUtils.AreEqual(a1 - b1, r1);

            float3x3 a2 = float3x3(-360.119629f, 7.80963135f, 437.428467f, -59.1991577f, 418.744324f, 183.142151f, 271.230347f, 496.208557f, 165.354919f);
            float3x3 b2 = float3x3(87.1369f, 430.02478f, 343.6571f, 121.029419f, -354.188171f, 249.052f, -2.22543335f, 22.4472656f, 478.112976f);
            float3x3 r2 = float3x3(-447.256531f, -422.215149f, 93.77136f, -180.228577f, 772.9325f, -65.90985f, 273.45578f, 473.7613f, -312.758057f);
            TestUtils.AreEqual(a2 - b2, r2);

            float3x3 a3 = float3x3(-227.403656f, -166.522858f, 356.142273f, 386.9264f, -394.638763f, 126.903259f, 97.21692f, -150.017853f, -227.250519f);
            float3x3 b3 = float3x3(-320.063f, -111.524109f, 222.228943f, -245.411072f, -119.902283f, -153.465668f, 374.1125f, 301.763428f, -281.430054f);
            float3x3 r3 = float3x3(92.65933f, -54.99875f, 133.91333f, 632.337463f, -274.736481f, 280.368927f, -276.895569f, -451.781281f, 54.1795349f);
            TestUtils.AreEqual(a3 - b3, r3);
        }

        [Test]
        public void float3x3_operator_sub_wide_scalar()
        {
            float3x3 a0 = float3x3(207.389587f, 248.457764f, -384.8239f, -205.344757f, -374.811554f, 191.642029f, 18.8562622f, -44.96161f, 480.857971f);
            float b0 = (-36.1124878f);
            float3x3 r0 = float3x3(243.502075f, 284.570251f, -348.711426f, -169.232269f, -338.699066f, 227.754517f, 54.96875f, -8.849121f, 516.970459f);
            TestUtils.AreEqual(a0 - b0, r0);

            float3x3 a1 = float3x3(16.3381958f, -35.5231f, 349.397766f, 439.077271f, 490.2223f, 195.024048f, -384.849426f, 189.05188f, 55.6027832f);
            float b1 = (-366.865448f);
            float3x3 r1 = float3x3(383.203644f, 331.342346f, 716.2632f, 805.942749f, 857.087769f, 561.8895f, -17.9839783f, 555.917358f, 422.468231f);
            TestUtils.AreEqual(a1 - b1, r1);

            float3x3 a2 = float3x3(-54.931488f, 316.8025f, -273.8067f, 256.8872f, 297.173645f, 101.829041f, 136.607971f, -19.7322083f, 336.589722f);
            float b2 = (53.0880737f);
            float3x3 r2 = float3x3(-108.019562f, 263.714417f, -326.894775f, 203.799133f, 244.085571f, 48.7409668f, 83.5199f, -72.82028f, 283.501648f);
            TestUtils.AreEqual(a2 - b2, r2);

            float3x3 a3 = float3x3(-51.8765564f, -467.055939f, -50.1670532f, 477.804565f, -60.82193f, 0.4111328f, 46.66095f, -19.241394f, 396.809753f);
            float b3 = (317.345764f);
            float3x3 r3 = float3x3(-369.222321f, -784.401733f, -367.512817f, 160.4588f, -378.1677f, -316.934631f, -270.6848f, -336.587158f, 79.46399f);
            TestUtils.AreEqual(a3 - b3, r3);
        }

        [Test]
        public void float3x3_operator_sub_scalar_wide()
        {
            float a0 = (-86.00824f);
            float3x3 b0 = float3x3(466.4251f, 298.486938f, -300.9501f, 315.38f, -381.092163f, -125.008362f, 58.4661865f, 214.7461f, -257.549438f);
            float3x3 r0 = float3x3(-552.43335f, -384.495178f, 214.941864f, -401.388245f, 295.083923f, 39.0001221f, -144.474426f, -300.754333f, 171.5412f);
            TestUtils.AreEqual(a0 - b0, r0);

            float a1 = (480.2246f);
            float3x3 b1 = float3x3(-443.355072f, 260.795044f, 29.6819458f, 139.857727f, -247.789948f, -248.466217f, 91.44513f, 86.3841553f, 373.8183f);
            float3x3 r1 = float3x3(923.5797f, 219.429565f, 450.542664f, 340.366882f, 728.0145f, 728.6908f, 388.77948f, 393.840454f, 106.406311f);
            TestUtils.AreEqual(a1 - b1, r1);

            float a2 = (260.411926f);
            float3x3 b2 = float3x3(114.353943f, -464.405457f, -109.741455f, -311.675354f, 107.864014f, -258.795166f, 14.0975342f, -461.970184f, 30.3108521f);
            float3x3 r2 = float3x3(146.057983f, 724.8174f, 370.153381f, 572.0873f, 152.547913f, 519.2071f, 246.314392f, 722.3821f, 230.101074f);
            TestUtils.AreEqual(a2 - b2, r2);

            float a3 = (63.70111f);
            float3x3 b3 = float3x3(-462.676758f, 39.75946f, 47.99817f, -177.6193f, 202.477051f, -289.3088f, -459.9254f, 248.386658f, 85.32971f);
            float3x3 r3 = float3x3(526.377869f, 23.94165f, 15.7029419f, 241.3204f, -138.77594f, 353.009918f, 523.6265f, -184.685547f, -21.6286011f);
            TestUtils.AreEqual(a3 - b3, r3);
        }

        [Test]
        public void float3x3_operator_mul_wide_wide()
        {
            float3x3 a0 = float3x3(-482.7138f, -407.2935f, 137.700562f, 208.541138f, 194.29657f, -484.242432f, 183.9873f, -241.33548f, 45.8687744f);
            float3x3 b0 = float3x3(-236.367889f, 260.7276f, -416.3863f, -364.4956f, -253.147522f, -369.202881f, 193.547913f, 169.0849f, 201.969666f);
            float3x3 r0 = float3x3(114098.047f, -106192.656f, -57336.625f, -76012.33f, -49185.6953f, 178783.7f, 35610.36f, -40806.1836f, 9264.101f);
            TestUtils.AreEqual(a0 * b0, r0);

            float3x3 a1 = float3x3(363.3261f, -328.118958f, -471.023071f, -262.682556f, -379.262756f, -374.090576f, 481.4474f, 104.628052f, 412.935425f);
            float3x3 b1 = float3x3(249.456055f, -308.193176f, -385.579651f, -183.2796f, 22.2756348f, -265.521423f, -95.67746f, 133.2544f, 148.311462f);
            float3x3 r1 = float3x3(90633.9f, 101124.023f, 181616.9f, 48144.3555f, -8448.318f, 99329.06f, -46063.6641f, 13942.1475f, 61243.06f);
            TestUtils.AreEqual(a1 * b1, r1);

            float3x3 a2 = float3x3(477.877258f, 20.3778076f, 291.995972f, -138.488312f, -393.464966f, 9.363098f, -131.942291f, 364.449646f, 390.615967f);
            float3x3 b2 = float3x3(249.284119f, 500.0055f, -19.3315735f, -36.69107f, 30.5238037f, -401.367f, 3.43725586f, 257.24176f, -290.971924f);
            float3x3 r2 = float3x3(119127.211f, 10189.0156f, -5644.7417f, 5081.284f, -12010.0479f, -3758.03857f, -453.5194f, 93751.67f, -113658.281f);
            TestUtils.AreEqual(a2 * b2, r2);

            float3x3 a3 = float3x3(418.797974f, -277.3448f, 11.4101563f, 474.876465f, -502.405029f, -222.59491f, 38.1690674f, 292.6125f, 203.2077f);
            float3x3 b3 = float3x3(337.47937f, 490.286133f, -191.0198f, -325.7345f, -52.1819763f, 123.435059f, -461.267059f, 122.353088f, 308.584656f);
            float3x3 r3 = float3x3(141335.672f, -135978.3f, -2179.566f, -154683.641f, 26216.4883f, -27476.0156f, -17606.1328f, 35802.043f, 62706.7773f);
            TestUtils.AreEqual(a3 * b3, r3);
        }

        [Test]
        public void float3x3_operator_mul_wide_scalar()
        {
            float3x3 a0 = float3x3(-96.31882f, -277.142273f, -239.93689f, 509.531433f, 255.8581f, 215.7315f, -455.50827f, -389.2433f, -338.29248f);
            float b0 = (-301.2072f);
            float3x3 r0 = float3x3(29011.9219f, 83477.25f, 72270.72f, -153474.547f, -77066.3047f, -64979.8867f, 137202.375f, 117242.883f, 101896.133f);
            TestUtils.AreEqual(a0 * b0, r0);

            float3x3 a1 = float3x3(53.7962646f, 135.354675f, -207.3501f, -383.9396f, -31.4252319f, 42.6761475f, 260.38385f, 176.867554f, 25.67212f);
            float b1 = (243.757324f);
            float3x3 r1 = float3x3(13113.2334f, 32993.6953f, -50543.1055f, -93588.09f, -7660.13037f, 10402.623f, 63470.47f, 43112.76f, 6257.767f);
            TestUtils.AreEqual(a1 * b1, r1);

            float3x3 a2 = float3x3(-290.5006f, -156.523315f, -208.402008f, 370.945068f, -341.59845f, 10.2703247f, -176.888763f, -61.0061035f, 186.279785f);
            float b2 = (207.091f);
            float3x3 r2 = float3x3(-60160.0625f, -32414.57f, -43158.18f, 76819.38f, -70741.97f, 2126.89185f, -36632.07f, -12633.8154f, 38576.8672f);
            TestUtils.AreEqual(a2 * b2, r2);

            float3x3 a3 = float3x3(-487.652222f, -317.7163f, -207.62735f, 388.8714f, -233.335327f, 128.415527f, 510.389526f, 267.576355f, -309.209656f);
            float b3 = (-129.376831f);
            float3x3 r3 = float3x3(63090.9f, 41105.13f, 26862.168f, -50310.95f, 30188.1855f, -16613.9941f, -66032.58f, -34618.18f, 40004.5664f);
            TestUtils.AreEqual(a3 * b3, r3);
        }

        [Test]
        public void float3x3_operator_mul_scalar_wide()
        {
            float a0 = (37.43219f);
            float3x3 b0 = float3x3(96.74756f, 492.185364f, -274.054565f, -452.870972f, 420.853333f, 102.182922f, -114.948883f, -351.120056f, -464.664978f);
            float3x3 r0 = float3x3(3621.473f, 18423.5762f, -10258.4629f, -16951.9531f, 15753.4619f, 3824.93066f, -4302.78857f, -13143.1924f, -17393.4277f);
            TestUtils.AreEqual(a0 * b0, r0);

            float a1 = (444.084839f);
            float3x3 b1 = float3x3(447.1053f, 130.829346f, -321.41333f, 445.301331f, 478.2436f, 358.571716f, -144.8901f, -438.893829f, -3.536438f);
            float3x3 r1 = float3x3(198552.672f, 58099.33f, -142734.781f, 197751.563f, 212380.734f, 159236.266f, -64343.5f, -194906.1f, -1570.47852f);
            TestUtils.AreEqual(a1 * b1, r1);

            float a2 = (-471.807556f);
            float3x3 b2 = float3x3(-42.5603943f, 119.911072f, 271.900024f, 239.684021f, 487.4414f, -79.18829f, -112.925659f, 161.370056f, 459.759155f);
            float3x3 r2 = float3x3(20080.3164f, -56574.95f, -128284.484f, -113084.734f, -229978.531f, 37361.6367f, 53279.18f, -76135.61f, -216917.844f);
            TestUtils.AreEqual(a2 * b2, r2);

            float a3 = (-337.195984f);
            float3x3 b3 = float3x3(-276.834534f, 469.723877f, -274.565155f, 506.7859f, 65.88257f, 495.855652f, -347.2796f, -343.606049f, -183.7038f);
            float3x3 r3 = float3x3(93347.49f, -158389f, 92582.2656f, -170886.172f, -22215.3379f, -167200.531f, 117101.289f, 115862.578f, 61944.1836f);
            TestUtils.AreEqual(a3 * b3, r3);
        }

        [Test]
        public void float3x3_operator_div_wide_wide()
        {
            float3x3 a0 = float3x3(-353.131439f, -102.799866f, 51.3191528f, -191.871674f, 8.041809f, -128.73764f, -136.0596f, -370.471f, -237.69455f);
            float3x3 b0 = float3x3(-178.739563f, -302.096283f, -199.405823f, 278.850769f, 502.3376f, -361.484833f, 353.121033f, -38.894928f, -75.76474f);
            float3x3 r0 = float3x3(1.97567582f, 0.34028843f, -0.257360339f, -0.688080132f, 0.0160087738f, 0.356135666f, -0.385305852f, 9.524919f, 3.1372714f);
            TestUtils.AreEqual(a0 / b0, r0);

            float3x3 a1 = float3x3(-432.546875f, 200.2655f, 361.4416f, -416.226135f, -450.0192f, -273.497437f, -286.908173f, -314.256042f, 177.762085f);
            float3x3 b1 = float3x3(-195.217834f, -405.034f, -394.23f, -375.8277f, -121.245483f, 447.623352f, 338.286255f, -405.5442f, -431.168945f);
            float3x3 r1 = float3x3(2.215714f, -0.4944412f, -0.9168292f, 1.107492f, 3.71163678f, -0.610999048f, -0.8481225f, 0.7748996f, -0.412279427f);
            TestUtils.AreEqual(a1 / b1, r1);

            float3x3 a2 = float3x3(97.6270142f, -68.10727f, -386.450745f, 263.699341f, -297.0271f, -501.777039f, -263.40686f, -451.080841f, -416.34552f);
            float3x3 b2 = float3x3(296.205139f, 437.939819f, 39.2106323f, 331.289734f, -310.619568f, 207.26947f, -223.293f, -480.0914f, 448.675964f);
            float3x3 r2 = float3x3(0.3295926f, -0.155517414f, -9.855764f, 0.795978f, 0.9562408f, -2.42089224f, 1.17964673f, 0.9395729f, -0.9279426f);
            TestUtils.AreEqual(a2 / b2, r2);

            float3x3 a3 = float3x3(-315.278748f, -28.1811218f, -397.870148f, -261.386658f, 40.3482056f, 277.245728f, 464.77124f, -336.641052f, 375.4781f);
            float3x3 b3 = float3x3(-460.097443f, -220.569855f, -84.85315f, 441.373779f, 72.41846f, 44.9760742f, -242.515381f, -451.302063f, -21.8996887f);
            float3x3 r3 = float3x3(0.6852434f, 0.127765059f, 4.688926f, -0.592211545f, 0.557153642f, 6.164294f, -1.91646087f, 0.7459329f, -17.14536f);
            TestUtils.AreEqual(a3 / b3, r3);
        }

        [Test]
        public void float3x3_operator_div_wide_scalar()
        {
            float3x3 a0 = float3x3(171.3424f, 0.103393555f, 57.8882446f, -256.130737f, 95.66968f, -290.3869f, -127.4487f, -79.7449f, 146.466858f);
            float b0 = (171.796814f);
            float3x3 r0 = float3x3(0.997355f, 0.000601836247f, 0.3369576f, -1.49089336f, 0.5568769f, -1.69029272f, -0.7418572f, -0.4641815f, 0.8525586f);
            TestUtils.AreEqual(a0 / b0, r0);

            float3x3 a1 = float3x3(-499.843567f, -453.2058f, -205.033813f, 481.738159f, 464.479065f, -293.4635f, -158.505585f, -289.5822f, 494.1286f);
            float b1 = (58.68634f);
            float3x3 r1 = float3x3(-8.517204f, -7.72250938f, -3.493723f, 8.2086935f, 7.91460276f, -5.00054169f, -2.700894f, -4.934406f, 8.419823f);
            TestUtils.AreEqual(a1 / b1, r1);

            float3x3 a2 = float3x3(203.583435f, 259.1192f, 460.844727f, 490.956238f, -280.478058f, -320.243866f, 192.41449f, 264.800842f, 226.852966f);
            float b2 = (180.9704f);
            float3x3 r2 = float3x3(1.12495434f, 1.431832f, 2.54652f, 2.712909f, -1.549856f, -1.76959252f, 1.06323731f, 1.46322739f, 1.25353634f);
            TestUtils.AreEqual(a2 / b2, r2);

            float3x3 a3 = float3x3(-192.235687f, -437.8922f, -413.232727f, 249.471863f, 313.035034f, 216.785583f, 383.7389f, 82.0233154f, 189.574646f);
            float b3 = (460.9765f);
            float3x3 r3 = float3x3(-0.4170184f, -0.949923038f, -0.896429062f, 0.5411813f, 0.6790694f, 0.4702747f, 0.8324478f, 0.177933827f, 0.4112458f);
            TestUtils.AreEqual(a3 / b3, r3);
        }

        [Test]
        public void float3x3_operator_div_scalar_wide()
        {
            float a0 = (-264.4425f);
            float3x3 b0 = float3x3(105.589111f, -142.349091f, -288.9489f, 39.644104f, -363.9914f, -149.718231f, -395.729126f, 258.7187f, -9.66626f);
            float3x3 r0 = float3x3(-2.50444865f, 1.85770416f, 0.9151877f, -6.670412f, 0.7265076f, 1.7662679f, 0.6682412f, -1.02212369f, 27.3572731f);
            TestUtils.AreEqual(a0 / b0, r0);

            float a1 = (117.725525f);
            float3x3 b1 = float3x3(-331.386536f, -509.986023f, 427.896484f, 467.617126f, -407.124634f, 252.690735f, 444.599365f, -88.31329f, 199.955017f);
            float3x3 r1 = float3x3(-0.355251372f, -0.230840683f, 0.2751262f, 0.251756221f, -0.289163351f, 0.465887785f, 0.264790148f, -1.33304417f, 0.58876f);
            TestUtils.AreEqual(a1 / b1, r1);

            float a2 = (-218.346924f);
            float3x3 b2 = float3x3(-13.4171753f, -296.131073f, 0.561340332f, -289.299316f, 196.218323f, 334.733459f, -282.392731f, -479.5036f, -473.439453f);
            float3x3 r2 = float3x3(16.2736874f, 0.737332046f, -388.974243f, 0.754744f, -1.11277544f, -0.652300835f, 0.7732031f, 0.455360353f, 0.4611929f);
            TestUtils.AreEqual(a2 / b2, r2);

            float a3 = (105.050781f);
            float3x3 b3 = float3x3(-287.6313f, 77.29932f, -210.894379f, -184.068237f, -315.148438f, 87.86688f, 101.590515f, 345.9364f, -146.318115f);
            float3x3 r3 = float3x3(-0.365227252f, 1.35901308f, -0.498120338f, -0.5707165f, -0.333337456f, 1.19556737f, 1.034061f, 0.3036708f, -0.71796155f);
            TestUtils.AreEqual(a3 / b3, r3);
        }

        [Test]
        public void float3x3_operator_mod_wide_wide()
        {
            float3x3 a0 = float3x3(-388.8125f, 181.681213f, -167.078735f, 432.820129f, -258.438965f, -170.110809f, 283.3183f, 122.716492f, 335.271f);
            float3x3 b0 = float3x3(436.944153f, 58.9400635f, -201.116241f, 279.289368f, -397.079773f, 377.899963f, 174.693848f, -228.176514f, -317.060181f);
            float3x3 r0 = float3x3(-388.8125f, 4.861023f, -167.078735f, 153.530762f, -258.438965f, -170.110809f, 108.624451f, 122.716492f, 18.2108154f);
            TestUtils.AreEqual(a0 % b0, r0);

            float3x3 a1 = float3x3(-503.608521f, 191.022522f, 289.742676f, -124.033722f, 259.274f, -274.358459f, -140.030792f, 324.577576f, -200.513092f);
            float3x3 b1 = float3x3(-417.4801f, -249.975952f, -397.571564f, -358.745453f, -198.15921f, 208.737122f, -12.1194153f, 25.2714233f, -194.1207f);
            float3x3 r1 = float3x3(-86.12842f, 191.022522f, 289.742676f, -124.033722f, 61.1147766f, -65.62134f, -6.717224f, 21.3204956f, -6.392395f);
            TestUtils.AreEqual(a1 % b1, r1);

            float3x3 a2 = float3x3(211.423157f, -51.2722168f, -230.633911f, 99.98938f, 399.18988f, 24.90326f, 50.92401f, -364.863678f, -252.626617f);
            float3x3 b2 = float3x3(-493.8718f, -312.3017f, -216.980591f, 413.570984f, -436.3944f, 3.491272f, -308.233429f, -441.375061f, 84.60083f);
            float3x3 r2 = float3x3(211.423157f, -51.2722168f, -13.65332f, 99.98938f, 399.18988f, 0.464355469f, 50.92401f, -364.863678f, -83.42496f);
            TestUtils.AreEqual(a2 % b2, r2);

            float3x3 a3 = float3x3(-281.2898f, -364.798523f, -329.026245f, 51.6098022f, 41.6478271f, 254.95105f, -458.6776f, -136.79303f, 72.40033f);
            float3x3 b3 = float3x3(373.163452f, 67.25275f, -320.333282f, 118.97937f, 44.8239746f, 354.0086f, -253.953125f, -195.162811f, 317.142822f);
            float3x3 r3 = float3x3(-281.2898f, -28.53479f, -8.692963f, 51.6098022f, 41.6478271f, 254.95105f, -204.724487f, -136.79303f, 72.40033f);
            TestUtils.AreEqual(a3 % b3, r3);
        }

        [Test]
        public void float3x3_operator_mod_wide_scalar()
        {
            float3x3 a0 = float3x3(-244.499634f, -211.8193f, -145.926788f, -304.9182f, 155.479492f, -133.907776f, 281.309631f, -226.535767f, 335.166138f);
            float b0 = (39.63495f);
            float3x3 r0 = float3x3(-6.68994141f, -13.6445618f, -27.0219421f, -27.4735718f, 36.574646f, -15.00293f, 3.86499023f, -28.3610229f, 18.0865479f);
            TestUtils.AreEqual(a0 % b0, r0);

            float3x3 a1 = float3x3(101.706482f, -285.4023f, -355.846863f, 259.378f, -330.871948f, -284.343567f, -102.683441f, -172.141754f, 206.41687f);
            float b1 = (319.4715f);
            float3x3 r1 = float3x3(101.706482f, -285.4023f, -36.3753662f, 259.378f, -11.4004517f, -284.343567f, -102.683441f, -172.141754f, 206.41687f);
            TestUtils.AreEqual(a1 % b1, r1);

            float3x3 a2 = float3x3(-416.713654f, 435.2975f, 132.552917f, 226.944092f, -306.1183f, 115.438477f, 281.882935f, -218.347443f, -140.0405f);
            float b2 = (-339.256653f);
            float3x3 r2 = float3x3(-77.457f, 96.04083f, 132.552917f, 226.944092f, -306.1183f, 115.438477f, 281.882935f, -218.347443f, -140.0405f);
            TestUtils.AreEqual(a2 % b2, r2);

            float3x3 a3 = float3x3(-462.3235f, 351.331055f, 321.047f, 346.0852f, -94.4077454f, 465.40918f, -367.197021f, -467.5106f, 415.2151f);
            float b3 = (-211.6087f);
            float3x3 r3 = float3x3(-39.10608f, 139.722351f, 109.438293f, 134.4765f, -94.4077454f, 42.1917725f, -155.588318f, -44.2931824f, 203.606384f);
            TestUtils.AreEqual(a3 % b3, r3);
        }

        [Test]
        public void float3x3_operator_mod_scalar_wide()
        {
            float a0 = (-66.94504f);
            float3x3 b0 = float3x3(-249.7761f, -396.073761f, 386.492065f, 168.939453f, -199.418243f, 261.7517f, 16.1274414f, 257.668152f, -75.78845f);
            float3x3 r0 = float3x3(-66.94504f, -66.94504f, -66.94504f, -66.94504f, -66.94504f, -66.94504f, -2.43527222f, -66.94504f, -66.94504f);
            TestUtils.AreEqual(a0 % b0, r0);

            float a1 = (170.9563f);
            float3x3 b1 = float3x3(-242.858276f, 425.9453f, 303.2724f, 3.033081f, -505.74353f, 461.957031f, 205.972778f, 270.040649f, -47.4807129f);
            float3x3 r1 = float3x3(170.9563f, 170.9563f, 170.9563f, 1.10375977f, 170.9563f, 170.9563f, 170.9563f, 170.9563f, 28.51416f);
            TestUtils.AreEqual(a1 % b1, r1);

            float a2 = (-150.254486f);
            float3x3 b2 = float3x3(149.499512f, -220.298035f, 31.1188354f, 400.635681f, 6.23144531f, -39.05075f, -71.9411f, -495.307129f, -86.7196045f);
            float3x3 r2 = float3x3(-0.754974365f, -150.254486f, -25.7791443f, -150.254486f, -0.6997986f, -33.1022339f, -6.372284f, -150.254486f, -63.53488f);
            TestUtils.AreEqual(a2 % b2, r2);

            float a3 = (-436.970062f);
            float3x3 b3 = float3x3(-472.294739f, -130.008759f, -251.516846f, 281.976379f, 388.86084f, 50.6152954f, 293.87085f, 123.744263f, 422.904358f);
            float3x3 r3 = float3x3(-436.970062f, -46.9437866f, -185.453217f, -154.993683f, -48.1092224f, -32.0477f, -143.099213f, -65.7372742f, -14.0657043f);
            TestUtils.AreEqual(a3 % b3, r3);
        }

        [Test]
        public void float3x3_operator_plus()
        {
            float3x3 a0 = float3x3(-418.829559f, -405.79895f, -34.04178f, 236.999268f, -459.8391f, 210.86145f, 293.742f, -373.015442f, -386.059845f);
            float3x3 r0 = float3x3(-418.829559f, -405.79895f, -34.04178f, 236.999268f, -459.8391f, 210.86145f, 293.742f, -373.015442f, -386.059845f);
            TestUtils.AreEqual(+a0, r0);

            float3x3 a1 = float3x3(4.95440674f, 504.474854f, -170.746521f, 439.5594f, -478.7494f, 116.400757f, 421.409668f, -258.596069f, 447.8661f);
            float3x3 r1 = float3x3(4.95440674f, 504.474854f, -170.746521f, 439.5594f, -478.7494f, 116.400757f, 421.409668f, -258.596069f, 447.8661f);
            TestUtils.AreEqual(+a1, r1);

            float3x3 a2 = float3x3(124.164368f, -65.94928f, 239.041809f, 498.449524f, -139.382538f, 279.072937f, 108.775818f, 37.9992065f, 136.812134f);
            float3x3 r2 = float3x3(124.164368f, -65.94928f, 239.041809f, 498.449524f, -139.382538f, 279.072937f, 108.775818f, 37.9992065f, 136.812134f);
            TestUtils.AreEqual(+a2, r2);

            float3x3 a3 = float3x3(-236.030029f, 342.2791f, 102.472229f, -161.454834f, -355.270874f, 141.314331f, 239.320862f, -494.6041f, 361.59198f);
            float3x3 r3 = float3x3(-236.030029f, 342.2791f, 102.472229f, -161.454834f, -355.270874f, 141.314331f, 239.320862f, -494.6041f, 361.59198f);
            TestUtils.AreEqual(+a3, r3);
        }

        [Test]
        public void float3x3_operator_neg()
        {
            float3x3 a0 = float3x3(148.461731f, -467.122681f, 132.04718f, 183.522644f, 473.701f, -407.9911f, -54.95877f, -382.9898f, -299.093384f);
            float3x3 r0 = float3x3(-148.461731f, 467.122681f, -132.04718f, -183.522644f, -473.701f, 407.9911f, 54.95877f, 382.9898f, 299.093384f);
            TestUtils.AreEqual(-a0, r0);

            float3x3 a1 = float3x3(-383.014069f, 168.735474f, 466.441528f, 171.902466f, -280.558319f, -78.857605f, 318.69635f, -39.9154053f, 140.340027f);
            float3x3 r1 = float3x3(383.014069f, -168.735474f, -466.441528f, -171.902466f, 280.558319f, 78.857605f, -318.69635f, 39.9154053f, -140.340027f);
            TestUtils.AreEqual(-a1, r1);

            float3x3 a2 = float3x3(132.195618f, 410.380554f, -237.056946f, -137.617828f, -245.349976f, 422.521362f, -434.57135f, 60.22223f, -466.5663f);
            float3x3 r2 = float3x3(-132.195618f, -410.380554f, 237.056946f, 137.617828f, 245.349976f, -422.521362f, 434.57135f, -60.22223f, 466.5663f);
            TestUtils.AreEqual(-a2, r2);

            float3x3 a3 = float3x3(426.894531f, -391.37207f, 423.237732f, 254.297546f, -114.848907f, 108.059692f, -507.9763f, -306.245728f, 219.66626f);
            float3x3 r3 = float3x3(-426.894531f, 391.37207f, -423.237732f, -254.297546f, 114.848907f, -108.059692f, 507.9763f, 306.245728f, -219.66626f);
            TestUtils.AreEqual(-a3, r3);
        }

        [Test]
        public void float3x3_operator_prefix_inc()
        {
            float3x3 a0 = float3x3(-139.842072f, -56.7436523f, -381.955322f, 509.796326f, -222.896332f, 210.319885f, -392.7315f, -300.1941f, 362.212769f);
            float3x3 r0 = float3x3(-138.842072f, -55.7436523f, -380.955322f, 510.796326f, -221.896332f, 211.319885f, -391.7315f, -299.1941f, 363.212769f);
            TestUtils.AreEqual(++a0, r0);

            float3x3 a1 = float3x3(401.6148f, -450.230164f, 243.546936f, 46.1920166f, -41.4972839f, 299.1855f, 154.356567f, -281.233276f, 200.706f);
            float3x3 r1 = float3x3(402.6148f, -449.230164f, 244.546936f, 47.1920166f, -40.4972839f, 300.1855f, 155.356567f, -280.233276f, 201.706f);
            TestUtils.AreEqual(++a1, r1);

            float3x3 a2 = float3x3(92.95776f, -295.587f, 18.4990845f, -215.711121f, 471.947266f, 257.0766f, 41.6259155f, 4.82543945f, 243.004761f);
            float3x3 r2 = float3x3(93.95776f, -294.587f, 19.4990845f, -214.711121f, 472.947266f, 258.0766f, 42.6259155f, 5.82543945f, 244.004761f);
            TestUtils.AreEqual(++a2, r2);

            float3x3 a3 = float3x3(-472.619019f, -477.459564f, 9.89147949f, -76.92285f, -29.7675781f, -387.177429f, 461.7093f, 13.699707f, -46.303772f);
            float3x3 r3 = float3x3(-471.619019f, -476.459564f, 10.8914795f, -75.92285f, -28.7675781f, -386.177429f, 462.7093f, 14.699707f, -45.303772f);
            TestUtils.AreEqual(++a3, r3);
        }

        [Test]
        public void float3x3_operator_postfix_inc()
        {
            float3x3 a0 = float3x3(-396.669739f, 511.20752f, 249.111267f, -128.817322f, -259.4903f, 278.008179f, -81.39343f, 66.71973f, 167.852112f);
            float3x3 r0 = float3x3(-396.669739f, 511.20752f, 249.111267f, -128.817322f, -259.4903f, 278.008179f, -81.39343f, 66.71973f, 167.852112f);
            TestUtils.AreEqual(a0++, r0);

            float3x3 a1 = float3x3(147.94397f, 41.03357f, 128.5304f, 73.15558f, -60.1323853f, -446.229767f, -296.937836f, 267.293823f, 446.2293f);
            float3x3 r1 = float3x3(147.94397f, 41.03357f, 128.5304f, 73.15558f, -60.1323853f, -446.229767f, -296.937836f, 267.293823f, 446.2293f);
            TestUtils.AreEqual(a1++, r1);

            float3x3 a2 = float3x3(49.2001953f, -510.864227f, 471.647461f, -171.013092f, 310.727356f, -298.917175f, 489.985f, 184.603455f, 290.69104f);
            float3x3 r2 = float3x3(49.2001953f, -510.864227f, 471.647461f, -171.013092f, 310.727356f, -298.917175f, 489.985f, 184.603455f, 290.69104f);
            TestUtils.AreEqual(a2++, r2);

            float3x3 a3 = float3x3(117.192322f, 412.3678f, -229.386566f, 239.596924f, 36.62433f, -80.70819f, -391.0335f, -478.227142f, 166.860474f);
            float3x3 r3 = float3x3(117.192322f, 412.3678f, -229.386566f, 239.596924f, 36.62433f, -80.70819f, -391.0335f, -478.227142f, 166.860474f);
            TestUtils.AreEqual(a3++, r3);
        }

        [Test]
        public void float3x3_operator_prefix_dec()
        {
            float3x3 a0 = float3x3(123.128723f, 256.84375f, 156.330811f, 461.737427f, 325.867981f, 392.015625f, 187.874146f, -236.225189f, 125.109619f);
            float3x3 r0 = float3x3(122.128723f, 255.84375f, 155.330811f, 460.737427f, 324.867981f, 391.015625f, 186.874146f, -237.225189f, 124.109619f);
            TestUtils.AreEqual(--a0, r0);

            float3x3 a1 = float3x3(469.844727f, 376.046875f, -363.0755f, -22.0289612f, 248.7901f, 168.095032f, 168.265625f, -190.284729f, 166.945557f);
            float3x3 r1 = float3x3(468.844727f, 375.046875f, -364.0755f, -23.0289612f, 247.7901f, 167.095032f, 167.265625f, -191.284729f, 165.945557f);
            TestUtils.AreEqual(--a1, r1);

            float3x3 a2 = float3x3(183.957947f, -460.739319f, 89.5698853f, -267.4298f, 201.756226f, -141.216888f, -217.4841f, 197.361755f, -213.544128f);
            float3x3 r2 = float3x3(182.957947f, -461.739319f, 88.5698853f, -268.4298f, 200.756226f, -142.216888f, -218.4841f, 196.361755f, -214.544128f);
            TestUtils.AreEqual(--a2, r2);

            float3x3 a3 = float3x3(180.7406f, 478.045532f, -454.566132f, -386.898346f, 387.857f, -315.110443f, -108.28656f, -286.317017f, -375.601563f);
            float3x3 r3 = float3x3(179.7406f, 477.045532f, -455.566132f, -387.898346f, 386.857f, -316.110443f, -109.28656f, -287.317017f, -376.601563f);
            TestUtils.AreEqual(--a3, r3);
        }

        [Test]
        public void float3x3_operator_postfix_dec()
        {
            float3x3 a0 = float3x3(379.6883f, 302.692871f, -176.07135f, -291.2527f, 470.567566f, -402.925964f, -63.65515f, 355.2611f, -27.8892212f);
            float3x3 r0 = float3x3(379.6883f, 302.692871f, -176.07135f, -291.2527f, 470.567566f, -402.925964f, -63.65515f, 355.2611f, -27.8892212f);
            TestUtils.AreEqual(a0--, r0);

            float3x3 a1 = float3x3(-100.761841f, 479.9452f, -200.304291f, -445.026947f, 407.420349f, 327.670349f, 48.06018f, -209.667969f, -38.43506f);
            float3x3 r1 = float3x3(-100.761841f, 479.9452f, -200.304291f, -445.026947f, 407.420349f, 327.670349f, 48.06018f, -209.667969f, -38.43506f);
            TestUtils.AreEqual(a1--, r1);

            float3x3 a2 = float3x3(283.9416f, 152.510681f, -287.2625f, -215.948029f, -407.046356f, 159.233582f, -359.456482f, 168.41394f, -278.933777f);
            float3x3 r2 = float3x3(283.9416f, 152.510681f, -287.2625f, -215.948029f, -407.046356f, 159.233582f, -359.456482f, 168.41394f, -278.933777f);
            TestUtils.AreEqual(a2--, r2);

            float3x3 a3 = float3x3(289.912842f, 470.716553f, -208.560608f, 145.896729f, -296.790955f, -274.570831f, -250.04126f, -70.85629f, -485.627838f);
            float3x3 r3 = float3x3(289.912842f, 470.716553f, -208.560608f, 145.896729f, -296.790955f, -274.570831f, -250.04126f, -70.85629f, -485.627838f);
            TestUtils.AreEqual(a3--, r3);
        }


    }
}
