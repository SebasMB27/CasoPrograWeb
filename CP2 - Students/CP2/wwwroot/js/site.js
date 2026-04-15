// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Background music playback and controls
document.addEventListener('DOMContentLoaded', function() {
    const backgroundMusic = document.getElementById('backgroundMusic');
    const playPauseBtn = document.getElementById('playPauseBtn');
    const playPauseIcon = document.getElementById('playPauseIcon');
    const volumeSlider = document.getElementById('volumeSlider');
    const volumeValue = document.getElementById('volumeValue');
    const musicControlPanel = document.getElementById('musicControlPanel');
    
    if (!backgroundMusic) return;
    
    // Ensure looping is enabled
    backgroundMusic.loop = true;
    
    // Set initial volume
    backgroundMusic.volume = volumeSlider.value / 100;
    
    // Restore saved playback position and state
    const savedTime = localStorage.getItem('musicPlaybackTime');
    const savedState = localStorage.getItem('musicPlaybackState'); // 'playing' or 'paused'
    const savedVolume = localStorage.getItem('musicVolume');
    
    // Restore volume if saved
    if (savedVolume) {
        const volume = parseFloat(savedVolume);
        backgroundMusic.volume = volume;
        if (volumeSlider) {
            volumeSlider.value = Math.round(volume * 100);
            if (volumeValue) volumeValue.textContent = Math.round(volume * 100) + '%';
        }
    }
    
    // Wait for audio metadata to load before setting time
    backgroundMusic.addEventListener('loadedmetadata', function() {
        if (savedTime) {
            const time = parseFloat(savedTime);
            // Only restore if time is valid and not past the duration
            if (!isNaN(time) && time < backgroundMusic.duration && time >= 0) {
                backgroundMusic.currentTime = time;
            }
        }
    });
    
    // If metadata is already loaded, set time immediately
    if (backgroundMusic.readyState >= 1) {
        if (savedTime) {
            const time = parseFloat(savedTime);
            if (!isNaN(time) && time < backgroundMusic.duration && time >= 0) {
                backgroundMusic.currentTime = time;
            }
        }
    }
    
    // Save playback time periodically (every 1 second)
    backgroundMusic.addEventListener('timeupdate', function() {
        if (!backgroundMusic.paused) {
            localStorage.setItem('musicPlaybackTime', backgroundMusic.currentTime.toString());
            localStorage.setItem('musicPlaybackState', 'playing');
        }
    });
    
    // Save state when paused
    backgroundMusic.addEventListener('pause', function() {
        localStorage.setItem('musicPlaybackState', 'paused');
        localStorage.setItem('musicPlaybackTime', backgroundMusic.currentTime.toString());
    });
    
    // Save state when playing
    backgroundMusic.addEventListener('play', function() {
        localStorage.setItem('musicPlaybackState', 'playing');
    });
    
    // Save volume when changed
    if (volumeSlider) {
        volumeSlider.addEventListener('input', function() {
            localStorage.setItem('musicVolume', backgroundMusic.volume.toString());
        });
    }
    
    // Save state before page unload (navigation, refresh, etc.)
    window.addEventListener('beforeunload', function() {
        localStorage.setItem('musicPlaybackTime', backgroundMusic.currentTime.toString());
        localStorage.setItem('musicPlaybackState', backgroundMusic.paused ? 'paused' : 'playing');
        localStorage.setItem('musicVolume', backgroundMusic.volume.toString());
    });
    
    // Intercept form submissions to save state
    document.addEventListener('submit', function(e) {
        localStorage.setItem('musicPlaybackTime', backgroundMusic.currentTime.toString());
        localStorage.setItem('musicPlaybackState', backgroundMusic.paused ? 'paused' : 'playing');
        localStorage.setItem('musicVolume', backgroundMusic.volume.toString());
    }, true); // Use capture phase to catch all form submissions
    
    // Play/Pause button functionality
    if (playPauseBtn && playPauseIcon) {
        playPauseBtn.addEventListener('click', function() {
            if (backgroundMusic.paused) {
                backgroundMusic.play().then(() => {
                    playPauseIcon.textContent = '⏸';
                }).catch(err => {
                    console.error('Error playing audio:', err);
                });
            } else {
                backgroundMusic.pause();
                playPauseIcon.textContent = '▶';
            }
        });
        
        // Update icon based on audio state
        backgroundMusic.addEventListener('play', function() {
            playPauseIcon.textContent = '⏸';
        });
        
        backgroundMusic.addEventListener('pause', function() {
            playPauseIcon.textContent = '▶';
        });
    }
    
    // Volume slider functionality
    if (volumeSlider && volumeValue) {
        volumeSlider.addEventListener('input', function() {
            const volume = this.value / 100;
            backgroundMusic.volume = volume;
            volumeValue.textContent = this.value + '%';
        });
    }
    
    // Try to restore playback state and play if it was playing before
    // Note: Most browsers require user interaction before autoplay
    if (savedState === 'playing') {
        const playPromise = backgroundMusic.play();
        
        if (playPromise !== undefined) {
            playPromise.then(() => {
                // Audio started playing successfully
                console.log('Background music resumed from saved position');
                if (playPauseIcon) playPauseIcon.textContent = '⏸';
            }).catch(error => {
                // Autoplay was prevented - user interaction required
                console.log('Autoplay prevented. Music will play after user interaction.');
                if (playPauseIcon) playPauseIcon.textContent = '▶';
            });
        }
    } else {
        // Was paused, just update icon
        if (playPauseIcon) playPauseIcon.textContent = '▶';
    }
    
    // Handle audio errors
    backgroundMusic.addEventListener('error', function(e) {
        console.error('Error loading audio file:', e);
    });
    
    // Dragging functionality for music control panel
    if (musicControlPanel) {
        let isDragging = false;
        let startX, startY, initialX, initialY;
        
        // Load saved position from localStorage
        const savedPosition = localStorage.getItem('musicControlPosition');
        if (savedPosition) {
            try {
                const pos = JSON.parse(savedPosition);
                musicControlPanel.style.left = pos.x + 'px';
                musicControlPanel.style.top = pos.y + 'px';
                musicControlPanel.style.right = 'auto';
            } catch (e) {
                console.error('Error loading saved position:', e);
            }
        }
        
        const dragHandle = musicControlPanel.querySelector('.music-control-drag-handle');
        const header = musicControlPanel.querySelector('.music-control-header');
        
        // Make the header draggable
        (dragHandle || header || musicControlPanel).addEventListener('mousedown', dragStart);
        document.addEventListener('mousemove', drag);
        document.addEventListener('mouseup', dragEnd);
        
        // Touch events for mobile
        (dragHandle || header || musicControlPanel).addEventListener('touchstart', dragStartTouch, { passive: false });
        document.addEventListener('touchmove', dragTouch, { passive: false });
        document.addEventListener('touchend', dragEnd);
        
        function dragStart(e) {
            // Don't start dragging if clicking on buttons or sliders
            if (e.target.closest('.music-control-btn') || 
                e.target.closest('.music-volume-slider') || 
                e.target.closest('.music-control-body')) {
                return;
            }
            
            isDragging = true;
            musicControlPanel.classList.add('dragging');
            
            const rect = musicControlPanel.getBoundingClientRect();
            initialX = rect.left;
            initialY = rect.top;
            startX = e.clientX;
            startY = e.clientY;
            
            document.body.style.userSelect = 'none';
            e.preventDefault();
        }
        
        function dragStartTouch(e) {
            // Don't start dragging if clicking on buttons or sliders
            if (e.target.closest('.music-control-btn') || 
                e.target.closest('.music-volume-slider') || 
                e.target.closest('.music-control-body')) {
                return;
            }
            
            isDragging = true;
            musicControlPanel.classList.add('dragging');
            
            const touch = e.touches[0];
            const rect = musicControlPanel.getBoundingClientRect();
            initialX = rect.left;
            initialY = rect.top;
            startX = touch.clientX;
            startY = touch.clientY;
            
            document.body.style.userSelect = 'none';
            e.preventDefault();
        }
        
        function drag(e) {
            if (!isDragging) return;
            e.preventDefault();
            
            const clientX = e.type === 'mousemove' ? e.clientX : e.touches[0].clientX;
            const clientY = e.type === 'mousemove' ? e.clientY : e.touches[0].clientY;
            
            const deltaX = clientX - startX;
            const deltaY = clientY - startY;
            
            let newX = initialX + deltaX;
            let newY = initialY + deltaY;
            
            // Keep panel within viewport bounds
            const rect = musicControlPanel.getBoundingClientRect();
            const maxX = window.innerWidth - rect.width;
            const maxY = window.innerHeight - rect.height;
            
            newX = Math.max(0, Math.min(newX, maxX));
            newY = Math.max(0, Math.min(newY, maxY));
            
            musicControlPanel.style.left = newX + 'px';
            musicControlPanel.style.top = newY + 'px';
            musicControlPanel.style.right = 'auto';
        }
        
        function dragTouch(e) {
            if (!isDragging) return;
            drag(e);
        }
        
        function dragEnd(e) {
            if (isDragging) {
                isDragging = false;
                musicControlPanel.classList.remove('dragging');
                document.body.style.userSelect = '';
                
                // Save position to localStorage
                const rect = musicControlPanel.getBoundingClientRect();
                localStorage.setItem('musicControlPosition', JSON.stringify({
                    x: rect.left,
                    y: rect.top
                }));
            }
        }
    }
});