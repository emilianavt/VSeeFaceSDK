using UnityEngine;

public interface IEffectOverride {
    void Register(IEffectApplier applier, int id);
}