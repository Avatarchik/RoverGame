using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ScionEngine;


public class ScionMenu : Menu
{
    public ScionPostProcess scion;

    private float defaultAdaptionSpeed;

    //bloom
    private bool defaultBloomOn;
    private float defaultBloomBrightness;
    private float defaultBloomDownsamples;
    private float defaultBloomIntensity;

    //chromatic aberration
    private bool defaultChromaticAberrationOn;
    private float defaultChromaticAberrationDistortion;
    private float defaultChromaticAberrationIntensity;

    //color grading
    private float defaultColorGradingFactor;

    //depth of field
    private bool defaultDepthTemporalSamplingOn;
    private float defaultDepthAdaptationSpeed;
    private float defaultDepthOfTemporalBlend;
    private int defaultDepthSteps;

    //exposure
    private float defaultExposureCompensation;
    private float defaultFNumber;
    private float defaultShutterSpeed;

    //grain
    private bool defaultGrainOn;
    private float defaultGrainIntensity;

    //ISO
    private float defaultISO;

    private bool defaultLensFlareOn;
    private float defaultLensFlareBlurStrength;
    private float defaultLensFlareDiffractionUVScale;
    private int defaultLensFlareDownsamples;
    private float defaultLensFlareGhostDispersal;
    private float defaultLensFlareGhostDistortion;
    private float defaultLensFlareGhostEdgeFade;
    private float defaultLensFlareGhostIntensity;
    private float defaultLensFlareHaloDistortion;
    private float defaultLensFlareHaloIntensity;
    private float defaultLensFlareHaloWidth;

    //COC
    private float defaultMaxCOCRadius;

    //vignette
    private bool defaultVignetteOn;
    private float defaultVignetteIntensity;
    private float defaultVignetteScale;

    public ScionPostProcess ScionInstance
    {
        get { return (ScionInstance != null) ? scion : scion = GameObject.FindObjectOfType<ScionPostProcess>() as ScionPostProcess; }
    }

    public override void Close()
    {
        base.Close();
    }


    public override void Open()
    {
        base.Open();
    }


    private void RestoreDefaults()
    {
        ScionInstance.adaptionSpeed                     = defaultAdaptionSpeed;

        //bloom
        ScionInstance.bloom                             = defaultBloomOn;
        ScionInstance.bloomBrightness                   = defaultBloomBrightness;
        ScionInstance.bloomDistanceMultiplier           = defaultBloomDownsamples;
        ScionInstance.bloomIntensity                    = defaultBloomIntensity;

        //chromatic aberration
        ScionInstance.chromaticAberration               = defaultChromaticAberrationOn;
        ScionInstance.chromaticAberrationDistortion     = defaultChromaticAberrationDistortion;
        defaultChromaticAberrationIntensity     = ScionInstance.chromaticAberrationIntensity;

        //color grading
        ScionInstance.colorGradingBlendFactor           = defaultColorGradingFactor;

        //depth of field
        ScionInstance.depthAdaptionSpeed                = defaultDepthAdaptationSpeed;
        ScionInstance.depthOfFieldTemporalBlend         = defaultDepthOfTemporalBlend;
        ScionInstance.depthOfFieldTemporalSteps         = defaultDepthSteps;
        ScionInstance.depthOfFieldTemporalSupersampling = defaultDepthTemporalSamplingOn;

        //exposure
        ScionInstance.exposureCompensation              = defaultExposureCompensation;
        ScionInstance.fNumber                           = defaultFNumber;
        ScionInstance.shutterSpeed                      = defaultShutterSpeed;

        //grain
        ScionInstance.grain                             = defaultGrainOn;
        ScionInstance.grainIntensity                    = defaultGrainIntensity;

        //ISO
        ScionInstance.ISO                               = defaultISO;

        //lens flare
        ScionInstance.lensFlare                         = defaultLensFlareOn;
        ScionInstance.lensFlareBlurStrength             = defaultLensFlareBlurStrength;
        ScionInstance.lensFlareDiffractionUVScale       = defaultLensFlareDiffractionUVScale;
        ScionInstance.lensFlareDownsamples              = defaultLensFlareDownsamples;
        ScionInstance.lensFlareGhostDispersal           = defaultLensFlareGhostDispersal;
        ScionInstance.lensFlareGhostDistortion          = defaultLensFlareGhostDistortion;
        ScionInstance.lensFlareGhostEdgeFade            = defaultLensFlareGhostEdgeFade;
        ScionInstance.lensFlareGhostIntensity           = defaultLensFlareGhostIntensity;
        ScionInstance.lensFlareHaloDistortion           = defaultLensFlareHaloDistortion;
        ScionInstance.lensFlareHaloIntensity            = defaultLensFlareHaloIntensity;
        ScionInstance.lensFlareHaloWidth                = defaultLensFlareHaloWidth;

        //COC
        ScionInstance.maxCoCRadius                      = defaultMaxCOCRadius;

        //vignette
        ScionInstance.vignette                          = defaultVignetteOn;
        ScionInstance.vignetteIntensity                 = defaultVignetteIntensity;
        ScionInstance.vignetteScale                     = defaultVignetteScale;
    }


    private void Awake()
    {
        defaultAdaptionSpeed                 = ScionInstance.adaptionSpeed;

        //bloom
        defaultBloomOn                       = ScionInstance.bloom;
        defaultBloomBrightness               = ScionInstance.bloomBrightness;
        defaultBloomDownsamples              = ScionInstance.bloomDistanceMultiplier;
        defaultBloomIntensity                = ScionInstance.bloomIntensity;

        //chromatic aberration
        defaultChromaticAberrationOn         = ScionInstance.chromaticAberration;
        defaultChromaticAberrationDistortion = ScionInstance.chromaticAberrationDistortion;
        defaultChromaticAberrationIntensity  = ScionInstance.chromaticAberrationIntensity;

        //color grading
        defaultColorGradingFactor            = ScionInstance.colorGradingBlendFactor;

        //depth of field
        defaultDepthAdaptationSpeed          = ScionInstance.depthAdaptionSpeed;
        defaultDepthOfTemporalBlend          = ScionInstance.depthOfFieldTemporalBlend;
        defaultDepthSteps                    = ScionInstance.depthOfFieldTemporalSteps;
        defaultDepthTemporalSamplingOn       = ScionInstance.depthOfFieldTemporalSupersampling;

        //exposure
        defaultExposureCompensation          = ScionInstance.exposureCompensation;
        defaultFNumber                       = ScionInstance.fNumber;
        defaultShutterSpeed                  = ScionInstance.shutterSpeed;

        //grain
        defaultGrainOn                       = ScionInstance.grain;
        defaultGrainIntensity                = ScionInstance.grainIntensity;

        //ISO
        defaultISO                           = ScionInstance.ISO;

        //lens flare
        defaultLensFlareOn                   = ScionInstance.lensFlare;
        defaultLensFlareBlurStrength         = ScionInstance.lensFlareBlurStrength;
        defaultLensFlareDiffractionUVScale   = ScionInstance.lensFlareDiffractionUVScale;
        defaultLensFlareDownsamples          = ScionInstance.lensFlareDownsamples;
        defaultLensFlareGhostDispersal       = ScionInstance.lensFlareGhostDispersal;
        defaultLensFlareGhostDistortion      = ScionInstance.lensFlareGhostDistortion;
        defaultLensFlareGhostEdgeFade        = ScionInstance.lensFlareGhostEdgeFade;
        defaultLensFlareGhostIntensity       = ScionInstance.lensFlareGhostIntensity;
        defaultLensFlareHaloDistortion       = ScionInstance.lensFlareHaloDistortion;
        defaultLensFlareHaloIntensity        = ScionInstance.lensFlareHaloIntensity;
        defaultLensFlareHaloWidth            = ScionInstance.lensFlareHaloWidth;

        //COC
        defaultMaxCOCRadius                  = ScionInstance.maxCoCRadius;

        //vignette
        defaultVignetteOn                    = ScionInstance.vignette;
        defaultVignetteIntensity             = ScionInstance.vignetteIntensity;
        defaultVignetteScale                 = ScionInstance.vignetteScale;
    }
}
