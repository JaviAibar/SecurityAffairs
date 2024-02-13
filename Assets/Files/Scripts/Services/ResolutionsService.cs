using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Files.Scripts
{

    internal class ResolutionsService : IResolutionsService
    {
        private Animator _resTextAnimator;
        private Text _resText;
        private readonly List<Resolution> _resolutions;
        private readonly int _maxRefreshRate = Screen.resolutions.OrderByDescending(r => r.refreshRate).FirstOrDefault().refreshRate;
        private int _selectedRes = 0;

        public ResolutionsService(Animator resTextAnimator, Text resText)
        {
            this._resTextAnimator = resTextAnimator;
            this._resText = resText;
            _selectedRes = 0;

            _resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == _maxRefreshRate).ToList();
            _selectedRes = _resolutions.Count - 1;
            Debug.Log($"Detected {_resolutions.Count} resolutions:" + string.Join(",", _resolutions.Select(e => $"({e.height}, {e.width})").ToList()));

            SetRes();
        }

        /* IEnumerator SetRes()
{
Resolution res = _resolutions[_selectedRes];
print($"Setting {(_selectedRes + 1)} res out of {_resolutions.Count}: selected {res.width} x {res.height}");
Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
resText.gameObject.SetActive(true);
resText.text = $"{res.width} x {res.height}";
yield return new WaitForSeconds(1.5f);
resText.gameObject.SetActive(false);
}
*/
        public void SetNextRes()
        {
            if (_selectedRes < _resolutions.Count - 1) ++_selectedRes;
            SetRes();
        }

        public void SetPrevRes()
        {
            if (_selectedRes > 0) --_selectedRes;
            SetRes();
        }


        public void SetRes()
        {
            Resolution res = _resolutions[_selectedRes];
            Debug.Log($"Setting {_selectedRes + 1} res out of {_resolutions.Count}: selected {res.width} x {res.height}");
            Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
            _resText.gameObject.SetActive(true);
            _resText.text = $"{res.width} x {res.height}";
            _resTextAnimator.SetTrigger(Constants.Show);
        }
    }
}
