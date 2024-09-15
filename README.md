# Inspiration

 Our project was inspired by the growing need for supplementary forms of therapy in healthcare. One of our team members is a researcher exploring the neuroscientific foundation of music therapy and how different forms of biofeedback can be used to measure the effect of music on our brains and mental state. We recognized the proven effectiveness of music, art, and dance therapies, and sought to harness the power of immersive environments to enhance these therapeutic approaches. Our goal is to directly incorporate the user’s biometric data as real-time analysis into their progress.

 # What It Does?

 Our project is a healthcare-focused virtual reality experience designed to guide users towards their desired emotional state. Users begin by selecting a mood goal such as calming down or feeling energized. The experience then immerses them in a virtual environment filled with beautiful, dynamic color gradients that correspond to their chosen mood. As users engage in light movement activities such as dancing, yoga, tai chi, or stretching, the system plays carefully curated music to support their mood goal. The experience is highly interactive, with various sonic elements controlled by the user's body movements. This interactivity helps users connect more deeply with themselves and enhances the immersive nature of the experience. The system monitors the user's heart rate in real-time, adjusting the experience until the user reaches a heart rate that aligns with their chosen mood goal. This combination of visual, auditory, and kinesthetic elements, coupled with biofeedback, creates a powerful tool for emotional regulation that can be used in various therapeutic contexts.

 # How We Built It

 We built our project using a combination of existing VR technology and event-sponsor APIs. To detect real-time heart rate data, we integrated the Terra API, which allows us to receive live biometric data from Apple Watches. For movement detection, we leveraged the Meta Quest 3's Movement SDK, which allowed us to capture user’s body movements and head displacement. Then, we import a combination of these data points into Unity and use them to create a dynamic visual environment. We utilized shader graphs in Unity, generating visuals that correlate with the user's chosen mood goal. 

We programmed different physical movements to control various sonic elements; for example, the vertical positioning of arms and hands dictates the pitch of certain audio elements, while head movements trigger drum beats. The entire experience was developed within the Unity game engine, with C# as our primary programming language for implementing the logic and integrations.

# Challenges

During the development process, we encountered several challenges. One significant hurdle was our initial plan to use the Suno API and OpenAI API to generate music based on the user's verbal input about their emotional state. However, we faced difficulties with the API usage for Suno, which forced us to pivot our approach to music generation. Another challenge we faced was the integration of Meta Quest and Unity with Mac environments, which proved to be less straightforward than we had anticipated. This required additional time and effort to ensure smooth compatibility across our development setup. Despite these obstacles, overcoming them provided valuable learning experiences and pushed us to find creative solutions.

# Accomplishments
We are proud of successfully integrating real-time biometric data from wearable devices into our VR experience, creating a seamless connection between the user's physical state and the virtual environment. Our team also takes pride in developing an intuitive and engaging user interface that allows for easy navigation and interaction within the VR space. Additionally, we're pleased with our implementation of dynamic visual and auditory elements that respond to user movements, creating a truly immersive and personalized experience. Lastly, we're proud of how we collaborated as a team, leveraging each member's strengths and supporting each other through the challenges we faced.

# What We Learned
This project provided a wealth of learning opportunities for our team. We gained practical experience in creating WebSockets to stream data from wearable devices using the Terra API. A part of our team had their first exposure to C# programming, expanding our collective skill set. We all delved into new territories, exploring Unity development, implementing back-end API calls and integrations, and programming in C#. Perhaps most importantly, we learned the value of rapid prototyping and iteration, embracing a "fail fast" mentality that allowed us to quickly identify and solve problems. This project also reinforced the importance of teamwork and knowledge sharing, as we helped each other learn and grow throughout the development process.

# What's Next?
Looking ahead, we have several exciting ideas to expand and enhance our project. One of our primary goals is to evolve the music generation aspect so that it's entirely controlled by the user's movements. We plan to utilize full-body tracking capabilities, even if it's just an estimate of movement, to allow different types of movement to control various music elements. This would create an ambient soundtrack generated solely through the user's body movements. We also aim to refine our heart rate integration, using the initial heart rate to set the starting BPM of the generated soundtrack and then gradually adjusting it to help the user naturally reach their final heart rate goal. To further personalize the experience, we want to incorporate more comprehensive user health data, either manually input or gathered via wearable devices. This would allow us to determine what an individual's average heart rate should be, taking into account factors such as age, fitness levels, genetics, medications, time of day, altitude, and overall health conditions. By implementing these enhancements, we believe our project can become an even more powerful and personalized tool for emotional regulation and well-being.


# By
- Jessica Sheng
- Sinchana Nama
- Brady Li
- Michael Wiradharma
