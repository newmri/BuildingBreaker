using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCoreLibrary.Animation
{
    public static class AnimatorEventExtension
    {
        private static AnimatorEventReceiver AttachReceiver(ref Animator animator)
        {
            return animator.gameObject.GetOrAddComponent<AnimatorEventReceiver>();
        }

        public static void SetInteger(this Animator animator, string name, int value, Action<string> onFinished)
        {
            animator.SetInteger(name, value);
            AttachReceiver(ref animator).OnStateEnd(name, onFinished);
        }

        public static void SetFloat(this Animator animator, string name, float value, Action<string> onFinished)
        {
            animator.SetFloat(name, value);
            AttachReceiver(ref animator).OnStateEnd(name, onFinished);
        }

        public static void SetBool(this Animator animator, string name, bool value, Action<string> onFinished)
        {
            animator.SetBool(name, value);
            AttachReceiver(ref animator).OnStateEnd(name, onFinished);
        }

        public static void SetTrigger(this Animator animator, string name, Action<string> onFinished)
        {
            animator.SetTrigger(name);
            AttachReceiver(ref animator).OnStateEnd(name, onFinished);
        }
    }

    [RequireComponent(typeof(Animator))]
    public class AnimatorEventReceiver : MonoBehaviour
    {
        #region Inspector

        public List<AnimationClip> animationClips = new List<AnimationClip>();

        #endregion

        private Animator _animator = null;
        private bool _isPlayingAnimator = false;

        private Dictionary<string, List<Action>> _startEvents = new Dictionary<string, List<Action>>();
        private Dictionary<string, List<Action>> _endEvents = new Dictionary<string, List<Action>>();
        private Dictionary<string, Coroutine> _coroutines = new Dictionary<string, Coroutine>();

        public void OnStateEnd(string name, Action<string> onFinished)
        {
            if (_coroutines.ContainsKey(name))
                StopCoroutine(_coroutines[name]);

            _coroutines[name] = StartCoroutine(OnStateEndCheck(name, onFinished));
        }

        public IEnumerator OnStateEndCheck(string name, Action<string> onFinished)
        {
            _isPlayingAnimator = true;

            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (!_isPlayingAnimator)
                {
                    // 다음 애니메이션 클립이 재생되는지 1프레임 더 기다림
                    yield return new WaitForEndOfFrame();
                    if (!_isPlayingAnimator) break;

                }
            }

            onFinished?.Invoke(name);
            _coroutines.Remove(name);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            foreach (var clip in _animator.runtimeAnimatorController.animationClips)
            {
                animationClips.Add(clip);

                var animationStartEvent = new AnimationEvent
                {
                    time = 0,
                    functionName = "AnimationStartHandler",
                    stringParameter = clip.name
                };

                clip.AddEvent(animationStartEvent);

                var animationEndEvent = new AnimationEvent
                {
                    time = clip.length,
                    functionName = "AnimationEndHandler",
                    stringParameter = clip.name
                };

                clip.AddEvent(animationEndEvent);
            }
        }

        private void AnimationStartHandler(string name)
        {
            if (_startEvents.TryGetValue(name, out var actions))
            {
                foreach (var action in actions)
                    action?.Invoke();

                actions.Clear();
            }

            _isPlayingAnimator = true;
        }

        private void AnimationEndHandler(string name)
        {
            if (_endEvents.TryGetValue(name, out var actions))
            {
                foreach (var action in actions)
                    action?.Invoke();

                actions.Clear();
            }

            _isPlayingAnimator = false;
        }
    }
}