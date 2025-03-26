import React, { useState, FormEvent } from 'react';
import { Container, Row, Col, Button, Card } from 'react-bootstrap';
import { 
  TextInput, 
  TextareaInput, 
  SelectInput, 
  NumberInput, 
  CheckboxInput, 
  DateInput 
} from '../base/Inputs'; // Adjust import path as needed

interface FormData {
  name: string;
  email: string;
  age: number | '';
  country: string;
  message: string;
  birthDate: string;
  newsletter: boolean;
}

const DemoForm: React.FC = () => {
  const [formData, setFormData] = useState<FormData>({
    name: '',
    email: '',
    age: '',
    country: '',
    message: '',
    birthDate: '',
    newsletter: false
  });

  const [editing, setEditing] = useState(true);

  const [errors, setErrors] = useState<Partial<Record<keyof FormData, string>>>({});

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.target;
    
    // Handle checkbox separately
    if (type === 'checkbox') {
      setFormData(prev => ({
        ...prev,
        [name]: (e.target as HTMLInputElement).checked
      }));
      return;
    }

    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const validateForm = (): boolean => {
    const newErrors: Partial<Record<keyof FormData, string>> = {};

    if (!formData.name.trim()) {
      newErrors.name = 'Name is required';
    }

    if (!formData.email.trim()) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = 'Email is invalid';
    }

    if (formData.age !== '' && (Number(formData.age) < 18 || Number(formData.age) > 120)) {
      newErrors.age = 'Age must be between 18 and 120';
    }

    if (!formData.country) {
      newErrors.country = 'Country is required';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    
    if (validateForm()) {
      // Form is valid, you can submit the data
      console.log('Form submitted:', formData);
      alert('Form submitted successfully!');
    } else {
      console.log('Form has errors');
    }
  };

  const handleCancel = () => {
    setEditing(false);
  };

  return (
    <Container className="mt-5">
      <Row className="justify-content-center">
        <Col md={8}>
          <Card>
            <Card.Header as="h2" className="text-center">
              Demo Registration Form
            </Card.Header>
            <Card.Body>
              <form onSubmit={handleSubmit}>
                <TextInput 
                  label="Full Name" 
                  name="name"
                  value={formData.name}
                  onChange={handleInputChange}
                  placeholder="Enter your full name"
                  required
                  editing={editing}
                  error={errors.name}
                />

                <TextInput 
                  label="Email Address" 
                  name="email"
                  type="email"
                  value={formData.email}
                  onChange={handleInputChange}
                  placeholder="Enter your email"
                  required
                  editing={editing}
                  error={errors.email}
                />

                <NumberInput 
                  label="Age" 
                  name="age"
                  value={formData.age}
                  onChange={handleInputChange}
                  min={13}
                  max={120}
                  placeholder="Enter your age"
                  editing={editing}
                  error={errors.age}
                />

                <SelectInput 
                  label="Country" 
                  name="country"
                  value={formData.country}
                  onChange={handleInputChange}
                  options={[
                    { value: '', label: 'Select a country' },
                    { value: 'us', label: 'United States' },
                    { value: 'ca', label: 'Canada' },
                    { value: 'uk', label: 'United Kingdom' },
                    { value: 'au', label: 'Australia' }
                  ]}
                  required
                  editing={editing}
                  error={errors.country}
                />

                <TextareaInput 
                  label="Additional Message" 
                  name="message"
                  value={formData.message}
                  onChange={handleInputChange}
                  placeholder="Enter any additional information"
                  editing={editing}
                />

                <DateInput 
                  label="Birth Date" 
                  name="birthDate"
                  value={formData.birthDate}
                  onChange={handleInputChange}
                  max={new Date().toISOString().split('T')[0]}
                  editing={editing}
                />

                <CheckboxInput 
                  label="Subscribe to newsletter" 
                  name="newsletter"
                  checked={formData.newsletter}
                  onChange={handleInputChange}
                  editing={editing}
                />

            <Row className="g-2">
                <Col>
                {editing && (
                    <Button 
                    variant="primary" 
                    type="submit" 
                    className="w-100"
                    >
                    Submit Registration
                    </Button>
                )}
                </Col>
                {editing && (
                    <Col>
                    <Button 
                        variant="danger" 
                        className="w-100" 
                        onClick={() => setEditing(false)}
                    >
                        Cancel
                    </Button>
                    </Col>
                )}
                </Row>
                {!editing && (
                    <Col>
                    <Button 
                        variant="info" 
                        className="w-100" 
                        onClick={() => setEditing(true)}
                    >
                        Edit
                    </Button>
                    </Col>
                )}
              </form>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default DemoForm;