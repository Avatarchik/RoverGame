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
        scion.adaptionSpeed                     = defaultAdaptionSpeed;

        //bloom
        scion.bloom                             = defaultBloomOn;
        scion.bloomBrightness                   = defaultBloomBrightness;
        scion.bloomDistanceMultiplier           = defaultBloomDownsamples;
        scion.bloomIntensity                    = defaultBloomIntensity;

        //chromatic aberration
        scion.chromaticAberration               = defaultChromaticAberrationOn;
        scion.chromaticAberrationDistortion     = defaultChromaticAberrationDistortion;
        defaultChromaticAberrationIntensity     = scion.chromaticAberrationIntensity;

        //color grading
        scion.colorGradingBlendFactor           = defaultColorGradingFactor;

        //depth of field
        scion.depthAdaptionSpeed                = defaultDepthAdaptationSpeed;
        scion.depthOfFieldTemporalBlend         = defaultDepthOfTemporalBlend;
        scion.depthOfFieldTemporalSteps         = defaultDepthSteps;
        scion.depthOfFieldTemporalSupersampling = defaultDepthTemporalSamplingOn;

        //exposure
        scion.exposureCompensation              = defaultExposureCompensation;
        scion.fNumber                           = defaultFNumber;
        scion.shutterSpeed                      = defaultShutterSpeed;

        //grain
        scion.grain                             = defaultGrainOn;
        scion.grainIntensity                    = defaultGrainIntensity;

        //ISO
        scion.ISO                               = defaultISO;

        //lens flare
        scion.lensFlare                         = defaultLensFlareOn;
        scion.lensFlareBlurStrength             = defaultLensFlareBlurStrength;
        scion.lensFlareDiffractionUVScale       = defaultLensFlareDiffractionUVScale;
        scion.lensFlareDownsamples              = defaultLensFlareDownsamples;
        scion.lensFlareGhostDispersal           = defaultLensFlareGhostDispersal;
        scion.lensFlareGhostDistortion          = defaultLensFlareGhostDistortion;
        scion.lensFlareGhostEdgeFade            = defaultLensFlareGhostEdgeFade;
        scion.lensFlareGhostIntensity           = defaultLensFlareGhostIntensity;
        scion.lensFlareHaloDistortion           = defaultLensFlareHaloDistortion;
        scion.lensFlareHaloIntensity            = defaultLensFlareHaloIntensity;
        scion.lensFlareHaloWidth                = defaultLensFlareHaloWidth;

        //COC
        scion.maxCoCRadius                      = defaultMaxCOCRadius;

        //vignette
        scion.vignette                          = defaultVignetteOn;
        scion.vignetteIntensity                 = defaultVignetteIntensity;
        scion.vignetteScale                     = defaultVignetteScale;
    }


    private void Awake()
    {
        defaultAdaptionSpeed                 = scion.adaptionSpeed;

        //bloom
        defaultBloomOn                       = scion.bloom;
        defaultBloomBrightness               = scion.bloomBrightness;
        defaultBloomDownsamples              = scion.bloomDistanceMultiplier;
        defaultBloomIntensity                = scion.bloomIntensity;

        //chromatic aberration
        defaultChromaticAberrationOn         = scion.chromaticAberration;
        defaultChromaticAberrationDistortion = scion.chromaticAberrationDistortion;
        defaultChromaticAberrationIntensity  = scion.chromaticAberrationIntensity;

        //color grading
        defaultColorGradingFactor            = scion.colorGradingBlendFactor;

        //depth of field
        defaultDepthAdaptationSpeed          = scion.depthAdaptionSpeed;
        defaultDepthOfTemporalBlend          = scion.depthOfFieldTemporalBlend;
        defaultDepthSteps                    = scion.depthOfFieldTemporalSteps;
        defaultDepthTemporalSamplingOn       = scion.depthOfFieldTemporalSupersampling;

        //exposure
        defaultExposureCompensation          = scion.exposureCompensation;
        defaultFNumber                       = scion.fNumber;
        defaultShutterSpeed                  = scion.shutterSpeed;

        //grain
        defaultGrainOn                       = scion.grain;
        defaultGrainIntensity                = scion.grainIntensity;

        //ISO
        defaultISO                           = scion.ISO;

        //lens flare
        defaultLensFlareOn                   = scion.lensFlare;
        defaultLensFlareBlurStrength         = scion.lensFlareBlurStrength;
        defaultLensFlareDiffractionUVScale   = scion.lensFlareDiffractionUVScale;
        defaultLensFlareDownsamples          = scion.lensFlareDownsamples;
        defaultLensFlareGhostDispersal       = scion.lensFlareGhostDispersal;
        defaultLensFlareGhostDistortion      = scion.lensFlareGhostDistortion;
        defaultLensFlareGhostEdgeFade        = scion.lensFlareGhostEdgeFade;
        defaultLensFlareGhostIntensity       = scion.lensFlareGhostIntensity;
        defaultLensFlareHaloDistortion       = scion.lensFlareHaloDistortion;
        defaultLensFlareHaloIntensity        = scion.lensFlareHaloIntensity;
        defaultLensFlareHaloWidth            = scion.lensFlareHaloWidth;

        //COC
        defaultMaxCOCRadius                  = scion.maxCoCRadius;

        //vignette
        defaultVignetteOn                    = scion.vignette;
        defaultVignetteIntensity             = scion.vignetteIntensity;
        defaultVignetteScale                 = scion.vignetteScale;
    }
}
